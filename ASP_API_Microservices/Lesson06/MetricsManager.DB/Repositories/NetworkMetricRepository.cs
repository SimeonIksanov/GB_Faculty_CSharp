using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;

namespace MetricsManager.DB.Repositories
{
    public class NetworkMetricRepository : GenericRepository<NetworkMetricEntity>, INetworkMetricRepository
    {
        public NetworkMetricRepository(AppDbContext context) : base(context)
        {
        }
    }
}
