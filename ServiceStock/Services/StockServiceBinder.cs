using Android.OS;

namespace ServiceStock.Services
{
    public class StockServiceBinder : Binder
    {
        private readonly StockService _service;

        public StockServiceBinder(StockService service)
        {
            _service = service;
        }

        public StockService GetStockService()
        {
            return _service;
        }
    }
}