using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<CpuMetric> CpuMetrics { get; set; }
        public DbSet<HddMetric> HddMetrics { get; set; }
        public DbSet<RamMetric> RamMetrics { get; set; }
        public DbSet<NetworkMetric> NetworkMetrics { get; set; }
        public DbSet<DotnetMetric> DotnetMetrics { get; set; }

    }
}
