using MetricsAgent.Controllers;
using MetricsAgent.DB;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerUnitTest : MetricControllerUnitTest<DotnetMetric, DotnetMetricsController>
    {
        public NetworkMetricsControllerUnitTest() : base()
        {
            _controller = new DotnetMetricsController(
                _repositoryMock.Object,
                _loggerMock.Object,
                _mapper);
        }
    }
}
