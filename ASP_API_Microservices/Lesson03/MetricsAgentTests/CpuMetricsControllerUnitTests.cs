using System;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Microsoft.Extensions.Logging;
using MetricsAgent.Responses;
using System.Collections.Generic;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController cpuController;
        private TimeSpan fromTime;
        private TimeSpan toTime;

        private Mock<ICpuMetricsRepository> _repositoryMock;

        public CpuMetricsControllerUnitTests()
        {
            _repositoryMock = new Mock<ICpuMetricsRepository>();
            var loggerMock = new Mock<ILogger<CpuMetricsController>>();

            cpuController = new CpuMetricsController(_repositoryMock.Object, loggerMock.Object);
            fromTime = TimeSpan.FromSeconds(0);
            toTime = TimeSpan.FromSeconds(100);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _repositoryMock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = cpuController.Create(
                new MetricsAgent.Requests.CpuMetricCreateRequest { Time = DateTime.Now, Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _repositoryMock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetCpuMetrics_returnsData()
        {
            DateTime now = DateTime.Now;
            var expectedItem = new CpuMetric { Time = now, Id = 1, Value = 10 };

            var expected = new List<CpuMetric>() { expectedItem };

            _repositoryMock.Setup(repository => repository.GetByTimePeriod(DateTime.MinValue, DateTime.MaxValue))
                           .Returns(expected);

            var actual = cpuController.GetByTimePerion(DateTime.MinValue, DateTime.MaxValue);
            Assert.NotNull(actual);

            if (actual is OkObjectResult result)
                Assert.IsType< List<CpuMetricDto>>(result.Value);
        }

        [Fact]
        public void GetCpuMetrics_returnsOk()
        {
            var actual = cpuController.GetMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(actual);
        }
    }
}
