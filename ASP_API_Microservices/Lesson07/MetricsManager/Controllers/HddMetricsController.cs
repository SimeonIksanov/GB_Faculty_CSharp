using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : BaseMetricsController<HddMetricEntity>
    {
        public HddMetricsController(
            ILogger<DotNetMetricsController> logger,
            IHddMetricRepository hddRepository
            )
            : base(logger, hddRepository)
        {
        }
    }
}