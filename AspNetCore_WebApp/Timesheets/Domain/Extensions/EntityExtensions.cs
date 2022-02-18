using System;
using Models;
namespace Domain.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsEmptyObject(this Entity entity)
        {
            return entity.Id == Guid.Empty;
        }
    }
}
