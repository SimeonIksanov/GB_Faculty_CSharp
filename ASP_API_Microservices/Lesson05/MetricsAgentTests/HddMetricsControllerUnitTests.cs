using MetricsAgent.Controllers;
using MetricsAgent.DB;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTests : MetricControllerUnitTest<HddMetric, HddMetricsController>
    {
        public HddMetricsControllerUnitTests() : base()
        {
            _controller = new HddMetricsController(
                _repositoryMock.Object,
                _loggerMock.Object,
                _mapper);
        }
    }
}
