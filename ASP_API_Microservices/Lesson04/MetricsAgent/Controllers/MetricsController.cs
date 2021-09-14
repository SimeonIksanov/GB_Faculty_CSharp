using AutoMapper;
using MetricsAgent.DB;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [ApiController]
    public abstract class MetricsController<T> : ControllerBase where T:BaseEntity,new()
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

		[HttpPost("create")]
		public virtual IActionResult Create([FromBody] CreateMetricRequest request)
		{
			_logger.LogInformation(string.Format("params: time {0}; value {1}", request.Time, request.Value));
			_repository.AddAsync(_mapper.Map<T>(request));

			return Ok();
		}

		[HttpGet("All")]
		public virtual async Task<IActionResult> GetAll()
		{
			return await GetByTimePeriod(DateTime.MinValue, DateTime.MaxValue);
		}

		[HttpGet("GetByTimePeriod")]
		public virtual async Task<IActionResult> GetByTimePeriod([FromQuery] DateTime fromTime, [FromQuery] DateTime toTime)
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
