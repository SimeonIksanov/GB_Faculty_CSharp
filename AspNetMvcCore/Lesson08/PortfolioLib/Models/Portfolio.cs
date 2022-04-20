using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioLib.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }

        public string Account { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

    }
}
