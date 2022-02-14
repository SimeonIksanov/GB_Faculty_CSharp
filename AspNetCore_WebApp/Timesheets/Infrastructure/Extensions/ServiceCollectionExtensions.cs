using System;
using Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultDatabase");

            services.AddDbContext<TimesheetDbContext>(
                options => options.UseSqlite(connectionString),
                ServiceLifetime.Scoped);
        }
    }
}
