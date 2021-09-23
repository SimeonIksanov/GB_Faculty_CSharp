using System;
using MetricsManager.DB;
using MetricsManager.DB.Repositories;
using MetricsManager.Entities.Interfaces;
using MetricsManager.Quartz;
using MetricsManager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsManager
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
            services.AddControllers();
            
            string conn = Configuration.GetConnectionString("DefaultDatabase");
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(conn), ServiceLifetime.Singleton);

            services.AddSingleton<IAgentRepository, AgentRepository>();
            services.AddSingleton<ICpuMetricRepository, CpuMetricRepository>();
            services.AddSingleton<IRamMetricRepository, RamMetricRepository>();
            services.AddSingleton<IHddMetricRepository, HddMetricRepository>();
            services.AddSingleton<INetworkMetricRepository, NetworkMetricRepository>();
            services.AddSingleton<IDotnetMetricRepository, DotnetMetricRepository>();

            services.AddScoped<AgentRegistratorService>();

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                    .AddTransientHttpErrorPolicy(
                        p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000))
                    );

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<MetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(MetricJob),
                cronExpression: "0/30 * * * * ?")); // запускать каждые 5 секунд
            services.AddHostedService<QuartzHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
