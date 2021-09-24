using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;

namespace MetricsManager.DB.Repositories
{
    public class RamMetricRepository : GenericRepository<RamMetricEntity>, IRamMetricRepository
    {
        public RamMetricRepository(AppDbContext context) : base(context)
        {
        }
    }
}
