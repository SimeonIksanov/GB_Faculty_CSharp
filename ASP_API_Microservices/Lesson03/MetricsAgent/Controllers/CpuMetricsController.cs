using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Models;
using MetricsAgent.DAL;
using MetricsAgent.Responses;
using MetricsAgent.Requests;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
	[ApiController]
	public class CpuMetricsController : ControllerBase
	{
		private ICpuMetricsRepository _repository;
		private readonly ILogger<CpuMetricsController> _logger;

		public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger)
        {
			_repository = repository;
			_logger = logger;
        }

		[HttpPost("create")]
		public IActionResult Create([FromBody] CpuMetricCreateRequest request)
		{
			_logger.LogInformation(string.Format("params: time {0}; value {1}", request.Time, request.Value));
			_repository.Create(new CpuMetric
				{
					Time = request.Time,
					Value = request.Value
				});

			return Ok();
		}

		[HttpGet("all")]
		public IActionResult GetAll()
		{
			var metrics = _repository.GetAll();

			var response = new AllCpuMetricsResponse()
			{
				Metrics = new List<CpuMetricDto>()
			};

			foreach (var metric in metrics)
			{
				response.Metrics.Add(new CpuMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
			}

			return Ok(response);
		}

		[HttpGet("GetByTimePeriod")]
		public IActionResult GetByTimePerion(DateTime fromTime, DateTime toTime)
        {
			_logger.LogInformation(string.Format("params: fromTime {0}; toTime {1}", fromTime, toTime));
			return Ok(_repository.GetByTimePeriod(fromTime, toTime)
								 .Select(m => new CpuMetricDto() { Id = m.Id,Time = m.Time, Value = m.Value })
								 .ToList()) ;
		}

		[HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
			_logger.LogInformation(string.Format("params: fromTime {0}; toTime {1}", fromTime.TotalSeconds, toTime.TotalSeconds));
			return Ok();
		}

		//[HttpGet("sql-test")]
		//public IActionResult TryToSqLite()
		//{
		//	string connectionString = "Data source=:memory:";
		//	string query = "SELECT SQLITE_VERSION()";
		//	string queryResult;
		//	using (var connection = new SQLiteConnection(connectionString))
		//	{
		//		connection.Open();
		//		using (var command = new SQLiteCommand(query, connection))
		//		{
		//			queryResult = command.ExecuteScalar().ToString();
		//		}
		//	}

		//	return Ok(queryResult);
		//}

		//[HttpGet("sql-read-write-test")]
		//public IActionResult TryToInsertAndRead()
		//{
		//	string connectionString = "Data source=:memory:";

		//	using (var connection = new SQLiteConnection(connectionString))
		//	{
		//		connection.Open();
		//		using (var command = new SQLiteCommand(connection))
		//		{
		//			command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
		//			command.ExecuteNonQuery();

		//			command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
		//			command.ExecuteNonQuery();

		//			command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(10,1)";
		//			command.ExecuteNonQuery();

		//			command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(50,2)";
		//			command.ExecuteNonQuery();

		//			command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(75,4)";
		//			command.ExecuteNonQuery();

		//			command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(90,5)";
		//			command.ExecuteNonQuery();

		//			string readQuery = "SELECT * FROM cpumetrics LIMIT 3";
		//			var returnArray = new CpuMetric[3];
		//			command.CommandText = readQuery;
		//			using (SQLiteDataReader reader = command.ExecuteReader())
		//			{
		//				var counter = 0;
		//				while (reader.Read())
		//				{
		//					returnArray[counter] = new CpuMetric
		//					{
		//						Id = reader.GetInt32(0), // читаем данные полученные из базы данных
		//						Value = reader.GetInt32(1), // преобразуя к целочисленному типу
		//						Time = reader.GetInt64(2)
		//					};
		//					counter++;
		//				}
		//			}
		//			return Ok(returnArray);
		//		}
		//	}
		//}
	}
}