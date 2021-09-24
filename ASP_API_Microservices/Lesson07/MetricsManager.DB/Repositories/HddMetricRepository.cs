using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;

namespace MetricsManager.DB.Repositories
{
    public class HddMetricRepository : GenericRepository<HddMetricEntity>, IHddMetricRepository
    {
        public HddMetricRepository(AppDbContext context) : base(context)
        {
        }
    }
}
