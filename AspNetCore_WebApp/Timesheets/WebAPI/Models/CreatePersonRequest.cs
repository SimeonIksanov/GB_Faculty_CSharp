using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CreatePersonRequest
    {
        [Required]
        [MaxLength(16, ErrorMessage = "no more then 16 caracters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(16, ErrorMessage = "no more then 16 caracters")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength]
        public string Company { get; set; }

        [Range(15, 110, ErrorMessage = "Age from 15 to 110")]
        public int Age { get; set; }
    }
}
