using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lesson01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudController : ControllerBase
    {
        private readonly Dictionary<DateTime, double> _holder;

        public CrudController(Dictionary<DateTime, double> holder)
        {
            _holder = holder;
        }

        [HttpGet]
        public IActionResult Read([FromQuery] DateTime since, [FromQuery] DateTime until)
        {
            if (until == DateTime.MinValue)
                until = DateTime.MaxValue;

            return Ok(_holder.Where(kvp => since < kvp.Key && kvp.Key < until)
                   .ToArray());
        }

        [HttpPut]
        public IActionResult Add([FromQuery] DateTime dateTime, double temp)
        {
            if (_holder.ContainsKey(dateTime))
                return BadRequest("dateTime already exists");

            _holder.Add(dateTime, temp);
            return Ok();
        }

        [HttpPatch]
        public IActionResult Modify([FromQuery] DateTime dateTime, double temp)
        {
            if (_holder.ContainsKey(dateTime))
            {
                _holder[dateTime] = temp;
                return Ok();
            }

            return BadRequest("No datetime found");
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] DateTime dateTime)
        {
            if (_holder.ContainsKey(dateTime))
                _holder.Remove(dateTime);
            return Ok();
        }
    }
}