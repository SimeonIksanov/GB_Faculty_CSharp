using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class Employee : Entity
    {
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Sheet> Sheets { get; set; }

    }
}
