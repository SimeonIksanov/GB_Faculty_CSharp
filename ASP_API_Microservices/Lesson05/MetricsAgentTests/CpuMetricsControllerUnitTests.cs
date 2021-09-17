using MetricsAgent.Controllers;
using MetricsAgent.DB;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests : MetricControllerUnitTest<CpuMetric, CpuMetricsController>
    {
        public CpuMetricsControllerUnitTests()
        {
            _controller = new CpuMetricsController(
                _repositoryMock.Object,
                _loggerMock.Object,
                _mapper);
        }


        [Fact]
        public async Task GetCpuMetrics_returnsData()
        {
            var expected = Enumerable.Range(1, 1).Select(x => new CpuMetric
            {
                Time = DateTime.Now,
                Id = 1,
                Value = 10
            }).AsQueryable();

            _repositoryMock.Setup(repository => repository.GetAll())
                           .Returns(expected);

            //var actual = await cpuController.GetByTimePeriod(DateTime.MinValue, DateTime.MaxValue);
            var actual = await _controller.GetAll();
            Assert.NotNull(actual);

            if (actual is OkObjectResult result)
                Assert.IsType<List<MetricDto>>(result.Value);
        }
    }
}
