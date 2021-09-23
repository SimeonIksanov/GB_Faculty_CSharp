using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;

namespace MetricsManager.DB.Repositories
{
    public class AgentRepository : GenericRepository<AgentEntity>, IAgentRepository
    {
        public AgentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
