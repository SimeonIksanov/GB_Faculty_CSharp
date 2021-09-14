using MetricsAgent.Controllers;
using MetricsAgent.DB;

namespace MetricsAgentTests
{
    public class DotnetMetricsControllerUnitTests : MetricControllerUnitTest<DotnetMetric, DotnetMetricsController>
    {
        public DotnetMetricsControllerUnitTests():base()
        {
            _controller = new DotnetMetricsController(
                _repositoryMock.Object,
                _loggerMock.Object,
                _mapper);
        }
    }
}
