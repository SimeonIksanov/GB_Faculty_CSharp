using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Service;
using Service.Extensions;
using Service.Models;
using Service.Repository;
using Xunit;

namespace ServiceTests
{
    public class PersonTests
    {
        TimeSheetsService _service;
        CancellationTokenSource _cts;
        IPersonRepository _repository;
        PersonParameters personParameters = new PersonParameters { PageNumber = 1, PageSize = 20 };

        public PersonTests()
        {
            _repository = new PersonRepositoryInMemory();
            _service = new TimeSheetsService(_repository);
            _cts = new CancellationTokenSource();
        }

        [Fact]
        public async Task GetAllPersons_notEmpty()
        {
            int personsInRepository = _repository.GetPersonsAsync(personParameters, _cts.Token).Result.Count();
            var persons = await _service.GetPersonsAsync(personParameters, _cts.Token);

            Assert.NotEmpty(persons);
            Assert.Equal(personsInRepository, persons.Count());
        }

        [Fact]
        public async Task GetPersonByName_ExistsOnlyOne()
        {
            var persons = await _service.GetPersonByNameAsync("Mohammad", personParameters, _cts.Token);
            Assert.NotEmpty(persons);
            Assert.Single(persons);
        }

        [Fact]
        public async Task GetPersonByName_NotExists()
        {
            var persons = await _service.GetPersonByNameAsync("Mohammad123", personParameters, _cts.Token);
            Assert.Empty(persons);
        }

        [Fact]
        public async Task GetPersonByName_ExistsTwo()
        {
            var persons = await _service.GetPersonByNameAsync("Melinda", personParameters, _cts.Token);
            Assert.Equal(2, persons.Count());
        }

        [Fact]
        public async Task GetPersonByID_Exists()
        {
            var person = await _service.GetPersonByIdAsync(12, _cts.Token);
            Assert.NotNull(person);
        }

        [Fact]
        public async Task GetPersonByID_NotExists()
        {
            Assert.True((await _service.GetPersonByIdAsync(55, _cts.Token)).IsEmptyObject());
        }

        [Fact]
        public async Task AddPerson_Test()
        {
            var _newPerson = new Person
            {
                FirstName = "William",
                LastName = "Gates",
                Company = "Microsoft",
                Age = 65,
                Email = "bill.gates@microsoft.com"
            };
            var p = await _service.AddPersonAsync(_newPerson, _cts.Token);

            Assert.NotNull(p);
            Assert.IsType<Person>(p);
            Assert.NotEqual(0, p.Id);
        }

        [Fact]
        public async Task UpdatePerson_Test()
        {
            var _newPerson = new Person
            {
                FirstName = "William",
                LastName = "Gates",
                Company = "Microsoft",
                Age = 65,
                Email = "bill.gates@microsoft.com"
            };
            _newPerson = await _service.AddPersonAsync(_newPerson, _cts.Token);

            _newPerson.FirstName = "Jeff";
            _newPerson.LastName = "Bezos";

            await _service.UpdatePersonAsync(_newPerson, _cts.Token);

            var updatedPerson = await _service.GetPersonByIdAsync(_newPerson.Id, _cts.Token);

            Assert.Equal(_newPerson.FirstName, updatedPerson.FirstName);
            Assert.Equal(_newPerson.LastName, updatedPerson.LastName);
        }

        [Fact]
        public async Task DeletePerson()
        {
            var _newPerson = new Person
            {
                FirstName = "William",
                LastName = "Gates",
                Company = "Microsoft",
                Age = 65,
                Email = "bill.gates@microsoft.com"
            };
            _newPerson = await _service.AddPersonAsync(_newPerson, _cts.Token);

            await _service.DeletePersonAsync(_newPerson.Id, _cts.Token);

            var deletedPerson = await _service.GetPersonByIdAsync(_newPerson.Id, _cts.Token);

            Assert.True(deletedPerson.IsEmptyObject());
        }
    }
}
