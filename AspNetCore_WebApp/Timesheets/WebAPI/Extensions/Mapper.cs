using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Service.Models;
using WebAPI.Models;

namespace WebAPI.Extensions
{
    public class Mapper
    {
        public static void CreateRequestToPerson(CreatePersonRequest requestData, Person person)
        {
            person.FirstName = requestData.FirstName;
            person.LastName = requestData.LastName;
            person.Company = requestData.Company;
            person.Age = requestData.Age;
            person.Email = requestData.Email;
        }

        public static void UpdateRequestToPerson(UpdatePersonRequest requestData, Person person)
        {
            person.Id = requestData.Id;
            person.FirstName = requestData.FirstName;
            person.LastName = requestData.LastName;
            person.Company = requestData.Company;
            person.Age = requestData.Age;
            person.Email = requestData.Email;
        }
    }
}
