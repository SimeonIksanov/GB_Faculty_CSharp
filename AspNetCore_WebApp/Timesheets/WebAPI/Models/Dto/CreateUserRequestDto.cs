using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Models.Dto
{
    public class CreateUserRequestDto
    {
        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Username length should be between 4 and 10")]
        public string Username { get; set; }

        [Required]
        [MinLength(16, ErrorMessage = "Password length should be longer then 16")]
        public string Password { get; set; }
    }
}
