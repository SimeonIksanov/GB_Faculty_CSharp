using AutoMapper;
using MetricsAgent.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : MetricsController<NetworkMetric>
    {
        public NetworkMetricsController(
            IDbRepository<NetworkMetric> repository,
            ILogger<NetworkMetricsController> logger,
            IMapper mapper)
            : base(repository, mapper)
        {
            _logger = logger;
        }
    }
}