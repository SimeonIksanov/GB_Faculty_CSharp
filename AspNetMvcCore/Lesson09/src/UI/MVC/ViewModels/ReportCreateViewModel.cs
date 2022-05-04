using MVC.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class ReportCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [MyDateTime(ErrorMessage = "Date in future")]

        public DateTime? SendAt { get; set; }

    }
}
