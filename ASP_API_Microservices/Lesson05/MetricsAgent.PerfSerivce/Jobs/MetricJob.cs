using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetricsAgent.DB;
using System.Diagnostics;

namespace MetricsAgent.PerfSerivce.Jobs
{
    public class MetricJob : IJob
    {
        private PerformanceCounter _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private PerformanceCounter _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        private PerformanceCounter _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        private PerformanceCounter _networkCounter = new PerformanceCounter("Network Adapter", "Bytes Total/sec", "intel[r] centrino[r] advanced-n 6200 agn");
        private PerformanceCounter _dotnetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps","_Global_");

        private readonly IDbRepository<CpuMetric> _cpuRepository;
        private readonly IDbRepository<HddMetric> _hddRepository;
        private readonly IDbRepository<RamMetric> _ramRepository;
        private readonly IDbRepository<NetworkMetric> _networkRepository;
        private readonly IDbRepository<DotnetMetric> _dotnetRepository;
        public MetricJob(
            IDbRepository<CpuMetric> cpuRepository,
            IDbRepository<HddMetric> hddRepository,
            IDbRepository<RamMetric> ramRepository,
            IDbRepository<NetworkMetric> networkRepository,
            IDbRepository<DotnetMetric> dotnetRepository
            )
        {
            _cpuRepository = cpuRepository;
            _hddRepository = hddRepository;
            _ramRepository = ramRepository;
            _networkRepository = networkRepository;
            _dotnetRepository = dotnetRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpu = Convert.ToInt32(_cpuCounter.NextValue());
            _cpuRepository.AddAsync(new CpuMetric() { Time = DateTime.Now, Value = cpu });

            var hdd = Convert.ToInt32(_hddCounter.NextValue());
            _hddRepository.AddAsync(new HddMetric() { Time = DateTime.Now, Value = hdd });

            var ram = Convert.ToInt32(_ramCounter.NextValue());
            _ramRepository.AddAsync(new RamMetric() { Time = DateTime.Now, Value = ram });

            var network = Convert.ToInt32(_networkCounter.NextValue());
            _networkRepository.AddAsync(new NetworkMetric() { Time = DateTime.Now, Value = network });

            var dotnet = Convert.ToInt32(_dotnetCounter.NextValue());
            _dotnetRepository.AddAsync(new DotnetMetric() { Time = DateTime.Now, Value = dotnet });

            return Task.CompletedTask;
        }


    }
}
