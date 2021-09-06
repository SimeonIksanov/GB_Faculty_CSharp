using System;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class UnitTest1
    {
        private CpuMetricsController cpuController;
        private DotnetMetricsController dotnetController;
        private HddMetricsController hddController;
        private NetworkMetricsController networkController;
        private RamMetricsController ramController;

        private TimeSpan fromTime;
        private TimeSpan toTime;

        public UnitTest1()
        {
            cpuController = new CpuMetricsController();
            dotnetController = new DotnetMetricsController();
            hddController = new HddMetricsController();
            networkController = new NetworkMetricsController();
            ramController = new RamMetricsController();

            fromTime = TimeSpan.FromSeconds(0);
            toTime = TimeSpan.FromSeconds(100);
        }

        [Fact]
        public void GetCpuMetrics_returnsOk()
        {
            var actual = cpuController.GetCpuMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(actual);
        }

        [Fact]
        public void GetDotnetErrorsCountMetrics_returnsOk()
        {
            var actual = dotnetController.GetDotnetErrorsCountMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(actual);
        }

        [Fact]
        public void GetHddLeftMetrics_returnsOk()
        {
            var actual = hddController.GetHddLeftMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(actual);
        }

        [Fact]
        public void GetNetworkMetrics_returnsOk()
        {
            var actual = networkController.GetNetworkMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(actual);
        }

        [Fact]
        public void GetRamAvailableMetrics_returnsOk()
        {
            var actual = ramController.GetRamAvailableMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(actual);
        }
    }
}
