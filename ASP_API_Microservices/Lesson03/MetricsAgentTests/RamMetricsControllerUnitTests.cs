using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController _controller;
        private TimeSpan fromTime = TimeSpan.FromSeconds(0);
        private TimeSpan toTime = TimeSpan.FromSeconds(100);
        public RamMetricsControllerUnitTests()
        {
            _controller = new RamMetricsController();
        }

        [Fact]
        public void GetMetrics_returnsOk()
        {
            var actual = _controller.GetMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(actual);
        }
    }
}
