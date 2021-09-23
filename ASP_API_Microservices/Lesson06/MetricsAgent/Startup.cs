using AutoMapper;
using MetricsAgent.DB;
using MetricsAgent.PerfSerivce;
using MetricsAgent.PerfSerivce.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsAgent
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
            services.AddDbContext<AppDbContext>(op => op.UseSqlite(conn),ServiceLifetime.Singleton);

            services.AddSingleton<IDbRepository<CpuMetric>, DbRepository<CpuMetric>>();
            services.AddSingleton<IDbRepository<RamMetric>, DbRepository<RamMetric>>();
            services.AddSingleton<IDbRepository<HddMetric>, DbRepository<HddMetric>>();
            services.AddSingleton<IDbRepository<NetworkMetric>, DbRepository<NetworkMetric>>();
            services.AddSingleton<IDbRepository<DotnetMetric>, DbRepository<DotnetMetric>>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<MetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(MetricJob),
                cronExpression: "0/5 * * * * ?")); // запускать каждые 5 секунд
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
