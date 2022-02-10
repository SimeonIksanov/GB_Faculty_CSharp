using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Service.Models;
using WebAPI.Models;

namespace WebAPI.Extensions
{
    public static class PersonMapper
    {
        public static Person Map(this Person person, CreateUpdatePersonRequest requestData)
        {
            person.Id = requestData.Id;
            person.FirstName = requestData.FirstName;
            person.LastName = requestData.LastName;
            person.Company = requestData.Company;
            person.Age = requestData.Age;
            person.Email = requestData.Email;

            return person;
        }
    }
}
