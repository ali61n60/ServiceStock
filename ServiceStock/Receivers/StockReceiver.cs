using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ServiceStock.Receivers
{
    public class StockReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            ((MainActivity)context).GetStocks();
            InvokeAbortBroadcast();
        }
    }
}