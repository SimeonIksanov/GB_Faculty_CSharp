using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotnetMetricsControllerUnitTests
    {
        private DotnetMetricsController _controller;
        private TimeSpan fromTime = TimeSpan.FromSeconds(0);
        private TimeSpan toTime = TimeSpan.FromSeconds(100);
        public DotnetMetricsControllerUnitTests()
        {
            _controller = new DotnetMetricsController();
        }

        [Fact]
        public void GetMetrics_returnsOk()
        {
            var actual = _controller.GetMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(actual);
        }
    }
}
