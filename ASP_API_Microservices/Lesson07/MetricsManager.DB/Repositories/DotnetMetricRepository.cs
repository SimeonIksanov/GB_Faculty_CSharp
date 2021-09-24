using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;

namespace MetricsManager.DB.Repositories
{
    public class DotnetMetricRepository : GenericRepository<DotnetMetricEntity>, IDotnetMetricRepository
    {
        public DotnetMetricRepository(AppDbContext context) : base(context)
        {
        }
    }
}
