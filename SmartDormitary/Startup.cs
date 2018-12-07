using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitary.Services;
using SmartDormitary.Data.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SmartDormitary.Data.Context;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;
using System;
using Microsoft.AspNetCore.Http;
using SmartDormitary.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using SmartDormitary.Services.Cron;
using FluentScheduler;
using SmartDormitary.Services.Cron.Jobs;
using SmartDormitary.Services.Hubs.Service;

namespace SmartDormitary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartDormitaryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LocalDBConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SmartDormitaryContext>()
                .AddDefaultTokenProviders();

            // External Login Providers
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "647090609020984";
                facebookOptions.AppSecret = "5a73b6698ce81b1782eb773cff221f45";
            });

            // Cookie Policy (GDPR)
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies 
                // is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // The TempData provider cookie is not essential. Make it essential
            // So TempData is functional when tracking is disabled.
            services.Configure<CookieTempDataProviderOptions>(options => {
                options.Cookie.IsEssential = true;
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            // Services & API
            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<ISensorsAPI, SensorsAPI>();
            services.AddScoped<ISensorsService, SensorsService>();
            services.AddScoped<ISensorTypesService, SensorTypesService>();
            services.AddScoped<IServiceProvider, ServiceProvider>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IHubService, HubService>();
            services.AddScoped<NotifyHub>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<ISensorJob, SensorJob>();
            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseAuthentication();
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotifyHub>("/notifyHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name : "areas",
                    template : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCookiePolicy();
        }
    }
}
