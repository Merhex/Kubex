using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Kubex.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _environment = environment;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";

            var status = exception switch
            {
                ApplicationException _      => context.Response.StatusCode = (int)HttpStatusCode.BadRequest,
                ArgumentNullException _     => context.Response.StatusCode = (int)HttpStatusCode.NotFound,
                _                           => context.Response.StatusCode = (int)HttpStatusCode.InternalServerError
            };
            
            if (status == (int)HttpStatusCode.InternalServerError)
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new { error = "An unexpected error happend. Please check your data formatting and contact the administrator if this error persists." })
                );
            else
                await context.Response.WriteAsync(result);
        }
    }
}