using AutoMapper;
using MetricsAgent.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : MetricsController<RamMetric>
    {
        public RamMetricsController(
            IDbRepository<RamMetric> repository,
            ILogger<RamMetricsController> logger,
            IMapper mapper)
            : base(repository, mapper)
        {
            _logger = logger;
        }
    }
}