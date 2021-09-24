using AutoMapper;
using MetricsAgent.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : MetricsController<HddMetric>
    {
        public HddMetricsController(
            IDbRepository<HddMetric> repository,
            ILogger<HddMetricsController> logger,
            IMapper mapper)
            : base(repository, mapper)
        {
            _logger = logger;
        }
    }
}