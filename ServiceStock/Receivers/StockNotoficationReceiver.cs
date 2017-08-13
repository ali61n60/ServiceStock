using Android.App;
using Android.Content;
using ServiceStock.Services;

namespace ServiceStock.Receivers
{
    [BroadcastReceiver]
    [IntentFilter(new[] { StockService.StockUpdatedAction }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class StockNotoficationReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            var notification = new Notification(Resource.Drawable.Icon, "New stock data is available");
            var pendingIntent = PendingIntent.GetActivity(context, 0, new Intent(context, typeof(MainActivity)), 0);
            notification.SetLatestEventInfo(context, "Stock Updated", "New stock data is available", pendingIntent);
            notificationManager.Notify(0, notification);
        }
    }
}