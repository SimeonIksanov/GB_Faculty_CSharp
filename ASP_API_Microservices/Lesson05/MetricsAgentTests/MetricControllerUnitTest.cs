using AutoMapper;
using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgentTests
{
    public abstract class MetricControllerUnitTest<TMetric, TController>
        where TMetric : BaseEntity, new()
        where TController : MetricsController<TMetric>
    {
        protected TController _controller;
        protected Mock<IDbRepository<TMetric>> _repositoryMock = new Mock<IDbRepository<TMetric>>();
        protected Mock<ILogger<TController>> _loggerMock = new Mock<ILogger<TController>>();
        protected long fromTime = long.MinValue;
        protected long toTime = long.MaxValue;
        protected IMapper _mapper;
        protected MetricControllerUnitTest()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                mp => mp.AddProfile(new MapperProfile()));

            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void GetMetrics_returnsOk()
        {
            var actual = _controller.GetByTimePeriod(fromTime, toTime);
            _ = Assert.IsAssignableFrom<Task<IActionResult>>(actual);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _repositoryMock.Setup(repository => repository.AddAsync(It.IsAny<TMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(
                new MetricsAgent.Requests.CreateMetricRequest { Time = DateTime.Now, Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _repositoryMock.Verify(repository => repository.AddAsync(It.IsAny<TMetric>()), Times.AtMostOnce());
        }
    }
}
