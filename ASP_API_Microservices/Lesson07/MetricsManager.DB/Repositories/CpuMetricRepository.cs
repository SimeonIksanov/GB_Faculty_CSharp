
using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;

namespace MetricsManager.DB.Repositories
{
    public class CpuMetricRepository : GenericRepository<CpuMetricEntity>, ICpuMetricRepository
    {
        public CpuMetricRepository(AppDbContext context) : base(context)
        {
        }
    }
}
