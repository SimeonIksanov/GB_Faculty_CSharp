using MetricsManager.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetricsManager.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<AgentEntity> Agents { get; set; }
        public DbSet<CpuMetricEntity> CpuMetrics { get; set; }

        public DbSet<RamMetricEntity> RamMetrics { get; set; }

        public DbSet<HddMetricEntity> HddMetrics { get; set; }

        public DbSet<NetworkMetricEntity> NetworkMetrics { get; set; }

        public DbSet<DotnetMetricEntity> DotnetMetrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgentEntity>()
                        .HasKey(k => k.AgentId);
        }
    }
}
