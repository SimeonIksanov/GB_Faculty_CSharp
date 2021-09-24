using AutoMapper;
using MetricsAgent.DB;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [ApiController]
    public abstract class MetricsController<T> : ControllerBase where T : BaseEntity, new()
    {
        protected readonly IDbRepository<T> _repository;
        protected ILogger<MetricsController<T>> _logger;
        protected readonly IMapper _mapper;

        protected MetricsController(
            IDbRepository<T> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики за все доступное время
        /// </summary>
        /// <remarks>
        /// пример запроса:
        /// бла бла бла
        /// </remarks>
        /// <param name="fromTime">Начальная метрика времени в секундах с 01.01.1970</param>
        /// <param name="toTime">Конечная метрика времени в секундах с 01.01.1970</param>
        /// <returns>Список метрик, которые были сохранены</returns>
        [HttpGet("all")]
        public virtual async Task<IActionResult> GetAll()
        {
            return await GetByTimePeriod(long.MinValue, long.MaxValue);
        }

        /// <summary>
        /// Получает метрики в указанном диапозоне
        /// </summary>
        /// <param name="fromTime">Начальная метрика времени в секундах с 01.01.1970</param>
        /// <param name="toTime">Конечная метрика времени в секундах с 01.01.1970</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапозоне времени</returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public virtual async Task<IActionResult> GetByTimePeriod([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            _logger.LogInformation(string.Format("params: fromTime {0}; toTime {1}", fromTime, toTime));
            var responce = new AllMetricsResponse
            {
                Metrics = await _repository.GetAll()
                                           .Where(item => item.Time > fromTime && item.Time < toTime)
                                           .Select(item => _mapper.Map<MetricDto>(item))
                                           .ToListAsync()
            };
            return Ok(responce);
        }
    }
}
