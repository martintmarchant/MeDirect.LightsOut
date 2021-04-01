using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Api.Middleware
{
    /// <summary>
    /// Catch All Errors and return friendly response
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// ErrorHandling (Middleware pipeline)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandlerAsync(context, ex);
            }
        }

        /// <summary>
        /// Generate consistant (Friendly) Error response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Task ExceptionHandlerAsync(HttpContext context, Exception exception)
        {
            // todo: define new error type and implement 
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            return context.Response.WriteAsync(result);
        }
    }
}
