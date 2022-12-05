using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioLib.Models
{
    public class Stock
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
