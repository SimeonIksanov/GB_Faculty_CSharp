using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Dto
{
    public class LoginRequestDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
