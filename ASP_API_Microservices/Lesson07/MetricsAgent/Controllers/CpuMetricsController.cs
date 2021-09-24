using AutoMapper;
using MetricsAgent.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : MetricsController<CpuMetric>
    {
        public CpuMetricsController(
            IDbRepository<CpuMetric> repository,
            ILogger<CpuMetricsController> logger,
            IMapper mapper)
            : base(repository, mapper)
        {
            _logger = logger;
        }
    }
}