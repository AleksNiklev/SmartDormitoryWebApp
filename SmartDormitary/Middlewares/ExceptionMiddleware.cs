using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartDormitary.Models;

namespace SmartDormitary.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
 
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
 
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(httpContext);
            }
        }
 
        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
 
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error was automatically logged, please bear with us."
            }.ToString());
        }
    }
}