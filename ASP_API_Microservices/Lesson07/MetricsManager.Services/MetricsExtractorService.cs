using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;
using MetricsManager.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Services
{
    public class MetricsExtractorService<TEntity> where TEntity : BaseMetricEntity
    {
        private readonly IGenericRepository<TEntity> _repository;

        public MetricsExtractorService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<List<MetricModel>> GetCounterValuesAsync(int agentId, long fromTime, long toTime)
        {
            return await _repository.GetAll()
                                    .Where(Entity => Entity.AgentId == agentId && Entity.Time>fromTime && Entity.Time<toTime)
                                    .Select(Entity => new MetricModel()
                                    {
                                        AgentId = Entity.AgentId,
                                        Id = Entity.Id,
                                        Value = Entity.Value,
                                        Time = DateTimeOffset.FromUnixTimeSeconds(Entity.Time).DateTime
                                    })
                                    .ToListAsync();
        }

        public async Task<List<MetricModel>> GetCounterValuesAsync(long fromTime, long toTime)
        {
            return await _repository.GetAll()
                                    .Where(Entity => Entity.Time > fromTime && Entity.Time < toTime)
                                    .Select(Entity => new MetricModel()
                                    {
                                        AgentId = Entity.AgentId,
                                        Value = Entity.Value,
                                        Time = DateTimeOffset.FromUnixTimeSeconds(Entity.Time).DateTime
                                    })
                                    .ToListAsync();
        }
    }
}
