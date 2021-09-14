using AutoMapper;
using MetricsAgent.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotnetMetricsController : MetricsController<DotnetMetric>
    {
        public DotnetMetricsController(
            IDbRepository<DotnetMetric> repository,
            ILogger<DotnetMetricsController> logger,
            IMapper mapper)
            : base(repository,  mapper)
        {
            _logger = logger;
        }
    }
}