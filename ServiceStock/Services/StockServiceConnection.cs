using Android.Content;
using Android.OS;

namespace ServiceStock.Services
{
    public class StockServiceConnection : Java.Lang.Object, IServiceConnection
    {
        private readonly MainActivity _mainActivity;

        public StockServiceConnection(MainActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            StockServiceBinder stockServiceBinder = service as StockServiceBinder;
            if (stockServiceBinder != null)
            {
                StockServiceBinder binder = (StockServiceBinder)service;
                _mainActivity.Binder = binder;
                _mainActivity.IsBound = true;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            _mainActivity.IsBound = false;
        }
    }
}