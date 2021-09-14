using AutoMapper;
using MetricsAgent.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddDbContext<AppDbContext>(op => op.UseSqlite(conn));

            services.AddScoped<IDbRepository<CpuMetric>, DbRepository<CpuMetric>>();
            services.AddScoped<IDbRepository<RamMetric>, DbRepository<RamMetric>>();
            services.AddScoped<IDbRepository<HddMetric>, DbRepository<HddMetric>>();
            services.AddScoped<IDbRepository<NetworkMetric>, DbRepository<NetworkMetric>>();
            services.AddScoped<IDbRepository<DotnetMetric>, DbRepository<DotnetMetric>>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
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
