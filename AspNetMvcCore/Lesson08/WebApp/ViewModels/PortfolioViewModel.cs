using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Controllers;

namespace WebApp.ViewModels
{
    public class PortfolioViewModel
    {
        public int PortfolioId { get; set; }

        [Required]
        [StringLength(6,ErrorMessage = "Length not more 6 ch")]
        [Display(Name ="Account name")]
        public string Account { get; set; }

        public virtual ICollection<StockViewModel> Stocks { get; set; }
    }
}
