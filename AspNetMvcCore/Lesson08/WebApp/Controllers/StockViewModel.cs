using System;

namespace WebApp.Controllers
{
    public class StockViewModel
    {
        public int StockId { get; set; }

        public string Ticker { get; set; }

        //public SecurityType Type { get; set; }

        //public CurrencyType Currency { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public DateTime TradeDate { get; set; }

        public int PortfolioId { get; set; }

        public double LastPrice { get; set; } //Current Price

        public double MarketValue => LastPrice * Quantity;

        public double TotalGain => MarketValue - PurchasePrice * Quantity;
    }
}
