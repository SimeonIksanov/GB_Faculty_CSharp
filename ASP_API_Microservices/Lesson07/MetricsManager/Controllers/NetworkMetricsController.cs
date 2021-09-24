using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : BaseMetricsController<NetworkMetricEntity>
    {
        public NetworkMetricsController(
            ILogger<NetworkMetricsController> logger,
            INetworkMetricRepository networkRepository
            )
            : base(logger, networkRepository)
        {
        }
    }
}