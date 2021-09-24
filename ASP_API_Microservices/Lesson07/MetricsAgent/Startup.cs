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
using System;
using System.IO;
using System.Reflection;

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
            services.AddDbContext<AppDbContext>(op => op.UseSqlite(conn), ServiceLifetime.Singleton);

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

            services.AddSwaggerGen(
                setupAction => {
                    setupAction.SwaggerDoc(
                        "v1",
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Version = "v1",
                            Title = "API сервиса агента сбора метрик",
                            Description = "Тут можно познакомиться с API нашего сервиса",
                            TermsOfService = new System.Uri("http://google.com/"),
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                            {
                                Email = "noEmail@mail.ru",
                                Name = "Vasia"
                            }
                        }
                    );
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    setupAction.IncludeXmlComments(xmlPath);
                }
            );
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

            app.UseSwagger();
            app.UseSwaggerUI(
                setupAction => {
                    setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "API сервиса агента метрик");
                }
            );
        }
    }
}
