using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }
    }
}