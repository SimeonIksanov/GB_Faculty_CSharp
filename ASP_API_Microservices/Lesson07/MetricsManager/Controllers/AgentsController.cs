using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult RegisterAgent(
            [FromBody] AgentInfo agentInfo,
            [FromServices] AgentRegistratorService agentRegistrator)
        {
            agentRegistrator.Register(agentInfo);
            return Ok();
        }

        [HttpPost("unregister")]
        public IActionResult UnregisterAgent(
            [FromBody] int agentId,
            [FromServices] AgentRegistratorService agentRegistrator)
        {
            agentRegistrator.Unregister(agentId);
            return Ok();
        }

        //[HttpPut("enable/{agentId}")]
        //public IActionResult EnableAgentById([FromRoute] int agentId)
        //{
        //    return Ok();
        //}

        //[HttpPut("disable/{agentId}")]
        //public IActionResult DisableAgentById([FromRoute] int agentId)
        //{
        //    return Ok();
        //}

        [HttpGet]
        public async Task<IActionResult> GetAgentsAsync([FromServices] AgentRegistratorService agentRegistrator)
        {
            return Ok(await agentRegistrator.GetRegisteredAsync());
        }
    }
}