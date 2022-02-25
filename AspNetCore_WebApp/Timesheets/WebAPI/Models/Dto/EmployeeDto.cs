using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAPI.ValidationAttributes;

namespace Models.Entities
{
    public class EmployeeDto
    {
        [Required, NotEmpty()]
        public Guid Id { get; set; }

        [Required, NotEmpty()]
        public Guid UserId { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Sheet> Sheets { get; set; }

    }
}
