using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;
using MetricsManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    public class BaseMetricsController<T> : ControllerBase where T :BaseMetricEntity
    {
        private readonly ILogger _logger;
        private readonly IGenericRepository<T> _repository;
        private readonly MetricsExtractorService<T> _mes;

        public BaseMetricsController(ILogger logger, IGenericRepository<T> repository)
        {
            _logger = logger;
            _repository = repository;
            _mes = new MetricsExtractorService<T>(_repository);
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgentAsync(
            [FromRoute] int agentId,
            [FromRoute] long fromTime,
            [FromRoute] long toTime)
        {
            _logger.LogInformation(string.Format(
                "GetMetricsFromAgent {0} request params: AgentId: {1}; FromTime: {2}; ToTime: {3}"
                , typeof(T).Name
                , agentId
                , fromTime.ToString()
                , toTime.ToString()));

            var counters = await _mes.GetCounterValuesAsync(agentId,fromTime, toTime);

            return Ok(counters);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster(
                [FromRoute] long fromTime,
                [FromRoute] long toTime)
        {
            _logger.LogInformation(string.Format(
                "GetMetricsFromAllCluster {0} request params: FromTime: {1}; ToTime: {2}"
                , typeof(T).Name
                , fromTime.ToString()
                , toTime.ToString()));
            var counters = await _mes.GetCounterValuesAsync(fromTime, toTime);

            return Ok(counters);
        }

    }
}
