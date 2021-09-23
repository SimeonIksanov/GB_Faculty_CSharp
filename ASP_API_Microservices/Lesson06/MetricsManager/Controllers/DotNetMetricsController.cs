using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : BaseMetricsController<DotnetMetricEntity>
    {
        public DotNetMetricsController(
            ILogger<DotNetMetricsController> logger,
            IDotnetMetricRepository dotnetRepository
            )
            : base(logger, dotnetRepository)
        {
        }
    }
}