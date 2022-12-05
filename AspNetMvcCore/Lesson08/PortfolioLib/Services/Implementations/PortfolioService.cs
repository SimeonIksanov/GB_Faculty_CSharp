using PortfolioLib.Models;
using PortfolioLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioLib.Services.Implementations
{
    public class PortfolioService : IPortfolioService
    {
        private List<Portfolio> _portfolios;
        private int _maxId = 2;
        public PortfolioService()
        {
            _portfolios = new List<Portfolio>
            {
                new Portfolio
                {
                    Account = "one",
                    PortfolioId = 1,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            StockId = 1,
                            Ticker = "SBER",
                            PortfolioId=1,
                            Quantity=1,
                            PurchasePrice = 1,
                            LastPrice=2,
                            TradeDate = DateTime.Now.AddDays(-3)
                        },
                        new Stock
                        {
                            StockId = 2,
                            Ticker = "GOOG",
                            PortfolioId=1,
                            Quantity=10,
                            PurchasePrice = 110,
                            LastPrice=211,
                            TradeDate = DateTime.Now.AddDays(-7)
                        }

                    }
                },
                new Portfolio
                {
                    Account = "two",
                    PortfolioId = 2,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            StockId = 10,
                            Ticker = "GAZP",
                            PortfolioId=2,
                            Quantity=2,
                            PurchasePrice = 2,
                            LastPrice=4,
                            TradeDate = DateTime.Now.AddDays(-6)
                        }
                    }
                }
            };
        }

        public Portfolio Create(Portfolio item)
        {
            var newPortfolio = new Portfolio
            {
                PortfolioId = ++_maxId,
                Account = item.Account,
                Stocks = new List<Stock>(),
            };
            _portfolios.Add(newPortfolio);
            return newPortfolio;
        }

        public Portfolio GetById(int id)
        {
            return _portfolios.FirstOrDefault(p => p.PortfolioId == id);
        }
        public IEnumerable<Portfolio> GetAll()
        {
            return _portfolios;
        }

        public Portfolio Update(int id, Portfolio item)
        {
            var portfolio = GetById(id);
            if (portfolio == null)
                throw new ArgumentException("portfolio not found");

            portfolio.Account = item.Account;
            return portfolio;
        }

        public void Delete(int id)
        {
            var portfolio = GetById(id);
            if (portfolio == null)
                throw new ArgumentException("portfolio not found");

            _portfolios.Remove(portfolio);
        }

    }
}
