using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog ingected into CpuMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId,
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(string.Format(
                "GetMetricsFromAgent request params: AgentId: {0}; FromTime: {1}; ToTime: {2}"
                , agentId
                , fromTime.ToString()
                , toTime.ToString()));
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
                [FromRoute] TimeSpan fromTime,
                [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(string.Format(
                "GetMetricsFromAllCluster request params: FromTime: {0}; ToTime: {1}"
                , fromTime.ToString()
                , toTime.ToString()));
            return Ok();
        }
    }
}