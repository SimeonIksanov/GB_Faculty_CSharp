using MetricsManager.Entities.Entities;
using MetricsManager.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Services
{
    public class AgentRegistratorService
    {
        private readonly IAgentRepository _agentRepository;

        public AgentRegistratorService(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public void Register(AgentInfo agentInfo)
        {
            _agentRepository.AddAsync(new AgentEntity
            {
                //AgentId = 0,
                AgentUrl = agentInfo.AgentAddress
            });
        }

        public void Unregister(int agentId)
        {
            var agentEntity = _agentRepository.GetAll().Where(x => x.AgentId == agentId).FirstOrDefault();
            if (agentEntity != null)
                _agentRepository.DeleteAsync(agentEntity);
        }

        public async Task<List<AgentInfo>> GetRegisteredAsync()
        {
            return await _agentRepository.GetAll()
                                         .Select(entity => new AgentInfo() {
                                            AgentAddress = entity.AgentUrl,
                                            AgentId = entity.AgentId })
                                         .ToListAsync();
        }
    }
}
