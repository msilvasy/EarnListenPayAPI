using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Handlers
{
    public class SpotifyHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SpotifyHeaderHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(!request.Headers.Contains("Authorization"))
            {
                var authorization = _httpContextAccessor.HttpContext.Request.Headers["Data"];
                request.Headers.Add("Authorization", $"Bearer {authorization}");
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
