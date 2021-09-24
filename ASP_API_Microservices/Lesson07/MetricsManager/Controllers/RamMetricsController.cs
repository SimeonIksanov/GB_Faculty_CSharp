using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : BaseMetricsController<RamMetricEntity>
    {
        public RamMetricsController(
            ILogger<RamMetricsController> logger,
            IRamMetricRepository ramRepository
            )
            : base(logger, ramRepository)
        {
        }
    }
}