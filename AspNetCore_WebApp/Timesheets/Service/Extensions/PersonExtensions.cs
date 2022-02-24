using System;
using Service.Models;

namespace Service.Extensions
{
    public static class PersonExtensions
    {
        public static bool IsEmptyObject(this Person person)
        {
            return person.Id.Equals(-1);
        }

        public static bool IsObjectNull(this Person person)
        {
            return person == null;
        }
    }
}
