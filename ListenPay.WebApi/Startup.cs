using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ListenPay.WebApi.Handlers;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.Models;
using ListenPay.WebApi.Models.Spotify;
using ListenPay.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Helpers;

namespace ListenPay.WebApi
{
    public class Startup
    {
        private readonly string _localOrigins = "_localOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Data.ListenPayDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ListenPayDatabase")));
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.Configure<SpotifyOptions>(Configuration.GetSection("Spotify"));
            services.Configure<SMTPOption>(Configuration.GetSection("SMTPOption"));

            services.AddTransient<SpotifyHeaderHandler>();
            services.AddHttpClient<ISpotifyService, SpotifyService>(c =>
             {
                 c.BaseAddress = new Uri(Configuration.GetSection("Spotify")["ApiBaseUrl"]);
                 c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             }).AddHttpMessageHandler<SpotifyHeaderHandler>();
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IKeyGeneratorService, KeyGeneratorService>();
            services.AddScoped<IYoutubeVideoService, YoutubeVideoService>();
            services.AddScoped<IYoutubeVideoCategory, YouTubeCategoryService>();
            services.AddScoped<IActivityLog, ActivityLogService>();
            services.AddScoped<ICompany, CompanyService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<ICategoryMatricsService, CategoryMatricsService>();
            services.AddScoped<IMediaPartner, MediaPartnerService>();

            services.AddCors(options =>
            {
                options.AddPolicy(_localOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.WithOrigins("http://localhost:4200",
                                        "http://localhost:4100", "http://localhost:4300");
                });
            });
            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1-beta", new OpenApiInfo { Title = "ListenPay API", Version = "v1-beta" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Data.ListenPayDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            var options = new RewriteOptions().AddRewrite("^app.*", "index.html", skipRemainingRules: true);
            app.UseMiddleware<JwtMiddleware>();
            app.UseRewriter(options);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1-beta/swagger.json", "LastenPay API V1 BETA");
                //c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
       
            app.UseAuthorization();

            app.UseCors(_localOrigins);

            app.UseDefaultFiles();

            app.UseStaticFiles();

            dbContext.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
