using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Extensions;
using Service.Models;
using WebAPI.Extensions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<Person>> GetPersonById([FromRoute] int id, CancellationToken token)
        {
            var person = await _service.GetPersonByIdAsync(id, token);
            if (person.IsEmptyObject())
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonByName([FromQuery] string name, [FromQuery] PersonParameters personParameters, CancellationToken token)
        {
            IEnumerable<Person> persons = await _service.GetPersonByNameAsync(name, personParameters, token);
            return Ok(persons);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons([FromQuery] PersonParameters personParameters, CancellationToken token)
        {
            IEnumerable<Person> persons = await _service.GetPersonsAsync(personParameters, token);
            return Ok(persons);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson([FromBody] CreatePersonRequest request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newPerson = new Person();
            Mapper.CreateRequestToPerson(request, newPerson);
            newPerson = await _service.AddPersonAsync(newPerson, token);
            return Ok(newPerson);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonRequest request, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updatedPerson = new Person();
            Mapper.UpdateRequestToPerson(request, updatedPerson);
            await _service.UpdatePersonAsync(updatedPerson, token);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id, CancellationToken token)
        {
            await _service.DeletePersonAsync(id, token);
            return Ok();
        }
    }
}