using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using ServiceStock.Receivers;
using ServiceStock.Services;

namespace ServiceStock
{
    [Activity(Label = "ServiceStock", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public bool IsBound;
        public StockServiceBinder Binder;
        public ArrayAdapter<Stock> ListAdapter { get; set; }

        private Intent _stockServiceIntent;
        private StockReceiver _stockReceiver;
        private StockServiceConnection _stockServiceConnection;

        private Button _button;
        private ListView _listView;
        private int _count = 1;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            _button = FindViewById<Button>(Resource.Id.MyButton);
            _button.Click += delegate { _button.Text = string.Format("{0} clicks!", _count++); };

            _listView = FindViewById<ListView>(Resource.Id.StockItemView);

            _stockServiceIntent = new Intent(ApplicationContext, typeof (StockService));
            _stockReceiver=new StockReceiver();
        }

        protected override void OnStart()
        {
            base.OnStart();
            IntentFilter intentFilter=new IntentFilter(StockService.StockUpdatedAction)
            {
                Priority = (int)IntentFilterPriority.HighPriority
            };
            RegisterReceiver(_stockReceiver, intentFilter);
            _stockServiceConnection=new StockServiceConnection(this);
            BindService(_stockServiceIntent, _stockServiceConnection, Bind.AutoCreate);
            ScheduleStockUpdates();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (IsBound)
            {
                UnbindService(_stockServiceConnection);
                IsBound = false;
            }
            UnregisterReceiver(_stockReceiver);
        }

        void ScheduleStockUpdates()
        {
            if (!IsAlarmSet())
            {
                AlarmManager alarm = (AlarmManager)GetSystemService(AlarmService);

                PendingIntent pendingServiceIntent = PendingIntent.GetService(this, 0, _stockServiceIntent,
                    PendingIntentFlags.CancelCurrent);
                alarm.SetRepeating(AlarmType.Rtc, 0, 15000, pendingServiceIntent);
            }
        }

        private bool IsAlarmSet()
        {
            return PendingIntent.GetBroadcast(this, 0, _stockServiceIntent, PendingIntentFlags.NoCreate) != null;
        }

        public void GetStocks()
        {
            if (IsBound)
            {
                RunOnUiThread(() =>
                {
                    List<Stock> stocks = Binder.GetStockService().GetStocks();
                    ListAdapter = new ArrayAdapter<Stock>(this, Android.Resource.Layout.SimpleListItem1, stocks);
                    _listView.Adapter = ListAdapter;
                });
            }
        }
    }
}

