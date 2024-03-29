﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Repository
{
    public interface IPersonRepository
    {
        Task<Person> AddPersonAsync(Person person, CancellationToken token);

        Task AddPersonRangeAsync(IEnumerable<Person> person, CancellationToken token);

        Task<Person> GetPersonByIdAsync(int id, CancellationToken token);

        Task<IEnumerable<Person>> GetPersonByNameAsync(string name, PersonParameters personParameters, CancellationToken token);

        Task<IEnumerable<Person>> GetPersonsAsync(PersonParameters personParameters, CancellationToken token);

        Task UpdatePersonAsync(Person person, CancellationToken token);

        Task DeletePersonAsync(int id, CancellationToken token);
    }
}
