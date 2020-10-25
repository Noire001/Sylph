using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hurricaneapi.Jobs;
using hurricaneapi.Models;
using hurricaneapi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;

namespace hurricaneapi
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
            services.AddRazorPages();
            services.AddControllers();
            services.AddTransient<HurricaneService>();

            services.AddQuartz(q =>
            {
                q.SchedulerId = "JobScheduler";
                q.SchedulerName = "Job Scheduler";
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                q.AddJob<HurricaneCsvJob>(j => j.WithIdentity("hurricaneJob"));
                q.AddTrigger(t => t
                    .WithIdentity("hurricaneJobTrigger")
                    .ForJob("hurricaneJob")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInHours(24).RepeatForever())
                );
            });
            
            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}