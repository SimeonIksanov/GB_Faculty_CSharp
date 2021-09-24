using MetricsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMetricsAgentClient _metricsAgentClient;

        public TestController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }

        [HttpGet]
        public IActionResult TestHttpClient()
        {
            var retVal = _metricsAgentClient.GetAllCpuMetrics(
                new Services.Models.AllMetricsApiRequestModel()
                {
                    ClientBaseAddress = "http://localhost:5000",
                    FromTime = 100,
                    ToTime = 9000000000,
                    AgentId = 6
                });
            return Ok(retVal);
        }
    }
}
