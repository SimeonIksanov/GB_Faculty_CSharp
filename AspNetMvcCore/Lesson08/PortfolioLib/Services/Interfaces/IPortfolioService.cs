using PortfolioLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioLib.Services.Interfaces
{
    public interface IPortfolioService
    {
        Portfolio Create(Portfolio item);

        IEnumerable<Portfolio> GetAll();
        Portfolio GetById (int id);

        Portfolio Update(int id, Portfolio item);

        void Delete(int id);
    }
}
