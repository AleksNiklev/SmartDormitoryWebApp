using Microsoft.AspNetCore.Http;
using SmartDormitary.Services.Cron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitary.Middlewares
{
    public class BackgroundJobMiddleware
    {
        private readonly RequestDelegate _next;

        public BackgroundJobMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IJobService cron)
        {
            cron.RunJob();
            await _next(httpContext);
        }
    }
}
