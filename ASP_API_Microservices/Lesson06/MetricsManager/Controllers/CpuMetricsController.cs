using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : BaseMetricsController<CpuMetricEntity>
    {
        public CpuMetricsController(
            ILogger<CpuMetricsController> logger,
            ICpuMetricRepository cpuRepository
            )
            : base(logger, cpuRepository)
        {
        }
    }
}