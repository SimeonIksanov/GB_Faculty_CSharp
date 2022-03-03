using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Repository
{
    public class PersonRepositoryInMemory : IPersonRepository
    {
        private List<Person> _storage;
        private int _lastId = 1;
        private Object lockObject = new object();

        public PersonRepositoryInMemory()
        {
            _storage = new List<Person>(50);
        }

        public async Task<Person> AddPersonAsync(Person person, CancellationToken token)
        {
            person.Id = _lastId++;
            await Task.Run(() => _storage.Add(person));
            return person;
        }

        public async Task AddPersonRangeAsync(IEnumerable<Person> persons, CancellationToken token)
        {
            foreach (var person in persons)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                person.Id = _lastId++;
                await Task.Run(() => _storage.Add(person));
            }
        }


        public async Task<Person> GetPersonByIdAsync(int id, CancellationToken token)
        {
            return await Task.Run(() => _storage.Where(p => p.Id == id).DefaultIfEmpty(new Person()).First(), token);
        }

        public async Task<IEnumerable<Person>> GetPersonByNameAsync(string name, PersonParameters personParameters, CancellationToken token)
        {
            return await Task.Run(() => _storage.Where(p => p.FirstName == name)
                                                .Skip((personParameters.PageNumber - 1) * personParameters.PageSize)
                                                .Take(personParameters.PageSize)
                                                .ToList(),
                                  token);
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync(PersonParameters personParameters, CancellationToken token)
        {
            return await Task.Run(() => _storage.Skip((personParameters.PageNumber - 1) * personParameters.PageSize)
                                                .Take(personParameters.PageSize)
                                                .ToList(),
                                  token);
        }


        public async Task UpdatePersonAsync(Person person, CancellationToken token)
        {
            await Task.Run(() =>
            {
                var personInRepo = _storage.First(p => p.Id == person.Id);

                // специально не использую Automapper
                personInRepo.FirstName = person.FirstName;
                personInRepo.LastName = person.LastName;
                personInRepo.Company = person.Company;
                personInRepo.Age = person.Age;
                personInRepo.Email = person.Email;
            }, token);
        }

        public async Task DeletePersonAsync(int id, CancellationToken token)
        {
            await Task.Run(() => _storage.Remove(_storage.First(p => p.Id == id)), token);
        }
    }
}
