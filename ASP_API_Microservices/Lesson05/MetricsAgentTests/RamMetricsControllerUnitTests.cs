using MetricsAgent.Controllers;
using MetricsAgent.DB;
namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTests : MetricControllerUnitTest<RamMetric, RamMetricsController>
    {
        public RamMetricsControllerUnitTests() : base()
        { 
            _controller = new RamMetricsController(
                _repositoryMock.Object,
                _loggerMock.Object,
                _mapper);
        }
    }
}
