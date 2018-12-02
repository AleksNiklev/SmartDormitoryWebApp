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
using SmartDormitary.Services.Cron;
using SmartDormitary.Services.Cron.Contracts;
using System;
using Microsoft.AspNetCore.Http;

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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SmartDormitaryContext>()
                .AddDefaultTokenProviders();

            // External Login Providers
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
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
            services.AddScoped<IJobScheduleService, JobScheduleService>();
            services.AddScoped<IServiceProvider, ServiceProvider>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Cron Jobs
            var sp = services.BuildServiceProvider();
            var jobService = sp.GetService<IJobScheduleService>();
            jobService.RunJobs();
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
