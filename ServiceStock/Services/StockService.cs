using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;

namespace ServiceStock.Services
{
    [Service]
    public class StockService : IntentService
    {
        IBinder _binder;
        List<Stock> _stocks;
        public const string StockUpdatedAction = "StocksUpdated";
        protected override void OnHandleIntent(Intent intent)
        {
            var stockSymbols = new List<string> { "AMZN", "FB", "GOOG", "AAPL", "MSFT", "IBM" };
            _stocks = updateStocks(stockSymbols);
            var stocksIntent = new Intent(StockUpdatedAction);
            SendOrderedBroadcast(stocksIntent, null);
        }


        //emulate data from web
        private List<Stock> updateStocks(List<string> stockSymbols)
        {
            List<Stock> tempStocks = new List<Stock>();
            Random random=new Random();
            foreach (string stockSymbol in stockSymbols)
            {
                Stock tempStock = new Stock
                {
                    Symbol = stockSymbol,
                    LastPrice = 100 + random.NextDouble()*1000
                };
                tempStocks.Add(tempStock);
            }
            return tempStocks;
        }

        public List<Stock> GetStocks()
        {
            return _stocks;
        }

        public override IBinder OnBind(Intent intent)
        {
            _binder = new StockServiceBinder(this);
            return _binder;
        }
        
    }
}