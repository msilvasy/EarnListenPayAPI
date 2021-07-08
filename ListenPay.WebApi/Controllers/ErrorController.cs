using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListenPay.WebApi.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if(context.Error is UnauthorizedAccessException)
            {
                return Problem(detail: context.Error.StackTrace, title: context.Error.Message, statusCode: 401);

            }
            else if(context.Error is HttpRequestException)
            {
                if(context.Error.Message.Contains("401"))
                {
                    return Problem(detail: context.Error.StackTrace, title: context.Error.Message, statusCode: 401);
                }
            }
            return Problem(detail: context.Error.StackTrace, title: context.Error.Message);
        }

        [HttpGet]
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}