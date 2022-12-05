using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioLib.Models;
using PortfolioLib.Services.Interfaces;
using System.Linq;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ILogger<PortfolioController> _logger;

        public PortfolioController(IPortfolioService portfolioService, ILogger<PortfolioController> logger)
        {
            _portfolioService = portfolioService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Querying all portfolios");
            var portfolios = _portfolioService.GetAll();
            return View(portfolios);
        }

        public IActionResult Details([FromRoute]int id)
        {
            _logger.LogInformation(string.Format("Querying portfolio {0}",id));

            var portfolio = _portfolioService.GetById(id);
            if (portfolio == null) return NotFound();

            var portfolioViewModel = new PortfolioViewModel
                {
                    Account = portfolio.Account,
                    PortfolioId = portfolio.PortfolioId,
                    Stocks = portfolio.Stocks.Select(s => new StockViewModel
                    {
                        LastPrice = s.LastPrice,
                        PortfolioId = s.PortfolioId,
                        PurchasePrice = s.PurchasePrice,
                        Quantity = s.Quantity,
                        StockId = s.StockId,
                        Ticker = s.Ticker,
                        TradeDate = s.TradeDate,
                    }).ToList()
                };
            return View(portfolioViewModel);
        }


        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(PortfolioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _portfolioService.Create(new Portfolio { Account = model.Account });
            return RedirectToAction("Index");
        }
        
        
        public IActionResult Edit(int id)
        {
            var portfolio = _portfolioService.GetById(id);
            if (portfolio == null) return NotFound();
            //return View(portfolio);
            var portfolioViewModel = new PortfolioViewModel
            {
                Account = portfolio.Account,
                PortfolioId = portfolio.PortfolioId,
                Stocks = portfolio.Stocks.Select(s => new StockViewModel
                {
                    LastPrice = s.LastPrice,
                    PortfolioId = s.PortfolioId,
                    PurchasePrice = s.PurchasePrice,
                    Quantity = s.Quantity,
                    StockId = s.StockId,
                    Ticker = s.Ticker,
                    TradeDate = s.TradeDate,
                }).ToList()
            };
            return View(portfolioViewModel);
        }
        [HttpPost]
        public IActionResult Edit(PortfolioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var updated = new Portfolio {PortfolioId = model.PortfolioId, Account = model.Account};
            _portfolioService.Update(model.PortfolioId, updated);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var portfolio = _portfolioService.GetById(id);
            if (portfolio == null) return NotFound();

            var portfolioViewModel = new PortfolioViewModel
            {
                Account = portfolio.Account,
                PortfolioId = portfolio.PortfolioId,
            };
            return View(portfolioViewModel);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var portfolio = _portfolioService.GetById(id);
            if (portfolio == null) return NotFound();

            _portfolioService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
