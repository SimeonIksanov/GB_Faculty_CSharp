using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Extensions;
using Service.Models;
using WebAPI.Extensions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private TimeSheetsService _service;

        public PersonsController(TimeSheetsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById([FromRoute] int id, CancellationToken token)
        {
            var person = await _service.GetPersonByIdAsync(id, token);
            if (person.IsEmptyObject())
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetPersonByName([FromQuery] string name, [FromQuery] PersonParameters personParameters, CancellationToken token)
        {
            IEnumerable<Person> persons = await _service.GetPersonByNameAsync(name, personParameters, token);
            return Ok(persons);
        }

        [HttpGet]
        public async Task<IActionResult> GetPersons([FromQuery] PersonParameters personParameters, CancellationToken token)
        {
            IEnumerable<Person> persons = await _service.GetPersonsAsync(personParameters, token);
            return Ok(persons);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreateUpdatePersonRequest request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newPerson = new Person().Map(request);
            newPerson = await _service.AddPersonAsync(newPerson, token);
            return Ok(newPerson);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] CreateUpdatePersonRequest request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updatedPerson = new Person().Map(request);
            await _service.UpdatePersonAsync(updatedPerson, token);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _service.DeletePersonAsync(id, token);
            return Ok();
        }
    }
}