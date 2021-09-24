using System;
using Quartz;
using System.Threading.Tasks;
using MetricsManager.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using MetricsManager.Entities.Entities;
using System.Collections.Generic;
using MetricsManager.Services;
using MetricsManager.Services.Models;
using System.Linq;

namespace MetricsManager.Quartz
{
    public class MetricJob : IJob
    {
        private readonly ICpuMetricRepository _cpuRepository;
        private readonly IHddMetricRepository _hddRepository;
        private readonly IRamMetricRepository _ramRepository;
        private readonly INetworkMetricRepository _networkRepository;
        private readonly IDotnetMetricRepository _dotnetRepository;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IAgentRepository _agentRepository;
        public MetricJob(
            ICpuMetricRepository cpuRepository,
            IHddMetricRepository hddRepository,
            IRamMetricRepository ramRepository,
            INetworkMetricRepository networkRepository,
            IDotnetMetricRepository dotnetRepository,
            IAgentRepository agentRepository,
            IMetricsAgentClient metricsAgentClient
            )
        {
            _cpuRepository = cpuRepository;
            _hddRepository = hddRepository;
            _ramRepository = ramRepository;
            _networkRepository = networkRepository;
            _dotnetRepository = dotnetRepository;
            _agentRepository = agentRepository;
            _metricsAgentClient = metricsAgentClient;
        }
        public Task Execute(IJobExecutionContext context)
        {
            foreach(var agent in GetAllAgentsAsync())
            {
                var request = new AllMetricsApiRequestModel()
                {
                    AgentId = agent.AgentId,
                    ClientBaseAddress = agent.AgentUrl.ToString(),
                    FromTime = _cpuRepository.GetAll().OrderBy(e=>e.Time).LastOrDefault()?.Time ?? 0,
                    ToTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };
                var metrics = _metricsAgentClient.GetAllCpuMetrics(request);
                SaveCpuMetrics(metrics, _cpuRepository);

                metrics = null;
                metrics = _metricsAgentClient.GetAllRamMetrics(request);
                SaveRamMetrics(metrics, _ramRepository);

                metrics = null;
                metrics = _metricsAgentClient.GetAllHddMetrics(request);
                SaveHddMetrics(metrics, _hddRepository);

                metrics = null;
                metrics = _metricsAgentClient.GetAllNetworkMetrics(request);
                SaveNetworkMetrics(metrics, _networkRepository);

                metrics = null;
                metrics = _metricsAgentClient.GetAllDotnetMetrics(request);
                SaveDotnetMetrics(metrics, _dotnetRepository);
            }
            return Task.CompletedTask;
        }

        private void SaveCpuMetrics(AllMetricsApiResponseModel allMetrics, ICpuMetricRepository repository)
        {
            //foreach (var metric in allMetrics.Metrics)
            //{
            //    repository.AddAsync(new CpuMetricEntity()
            //    {
            //        AgentId = metric.AgentId,
            //        Time = new DateTimeOffset(metric.Time).ToUnixTimeSeconds()
            //    });
            //}
            var entities = allMetrics.Metrics.Select(x => new CpuMetricEntity()
            {
                AgentId = x.AgentId,
                Time = new DateTimeOffset(x.Time, TimeSpan.FromSeconds(0)).ToUnixTimeSeconds()
            }).ToList();
            if (entities.Count > 0)
            {
                repository.AddRangeAsync(entities);
            }
        }

        private void SaveRamMetrics(AllMetricsApiResponseModel allMetrics, IRamMetricRepository repository)
        {
            foreach (var metric in allMetrics.Metrics)
            {
                repository.AddAsync(new RamMetricEntity()
                {
                    AgentId = metric.AgentId,
                    Time = new DateTimeOffset(metric.Time).ToUnixTimeSeconds()
                });
            }
        }

        private void SaveHddMetrics(AllMetricsApiResponseModel allMetrics, IHddMetricRepository repository)
        {
            foreach (var metric in allMetrics.Metrics)
            {
                repository.AddAsync(new HddMetricEntity()
                {
                    AgentId = metric.AgentId,
                    Time = new DateTimeOffset(metric.Time).ToUnixTimeSeconds()
                });
            }
        }

        private void SaveNetworkMetrics(AllMetricsApiResponseModel allMetrics, INetworkMetricRepository repository)
        {
            foreach (var metric in allMetrics.Metrics)
            {
                repository.AddAsync(new NetworkMetricEntity()
                {
                    AgentId = metric.AgentId,
                    Time = new DateTimeOffset(metric.Time).ToUnixTimeSeconds()
                });
            }
        }
        private void SaveDotnetMetrics(AllMetricsApiResponseModel allMetrics, IDotnetMetricRepository repository)
        {
            foreach (var metric in allMetrics.Metrics)
            {
                repository.AddAsync(new DotnetMetricEntity()
                {
                    AgentId = metric.AgentId,
                    Time = new DateTimeOffset(metric.Time).ToUnixTimeSeconds()
                });
            }
        }
        private List<AgentEntity> GetAllAgentsAsync()
        {
            return _agentRepository.GetAll().ToListAsync().Result;
        }
    }
}
