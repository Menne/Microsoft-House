using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V7.App;
using Android.Util;
using Gcm.Client;
using MicrosoftHouse.Droid;
using MicrosoftHouse.Models;
using System.Diagnostics;
using Xamarin.Forms;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
namespace TaskList.Droid.Services
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class GcmHandler : GcmBroadcastReceiverBase<GcmService>
    {
        // Replace with your Sender ID from the Firebase Console
        public static string[] SenderId = new string[] { "105452590734" };

    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }

        public GcmService() : base(GcmHandler.SenderId)
        {

        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info("GcmService", $"Message {intent.ToString()}");
            var op = intent.Extras.GetString("op");
            if (op == "eventsync")
            {
                var syncMessage = new PushToSync()
                {
                    Message = intent.Extras.GetString("message") ?? "Unknown Message",
                };
                MessagingCenter.Send<PushToSync>(syncMessage, "ItemsChanged");
              
            }
            var message = intent.Extras.GetString("message") ?? "Unknown Message";
            CreateNotification("Microsoft House", message, op);

        }

        private void CreateNotification(string title, string msg, string parameter = null)
        {
            var startupIntent = new Intent(this, typeof(MainActivity));
            startupIntent.PutExtra("param", parameter);

            var stackBuilder = TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(startupIntent);

            var pendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.OneShot);
            var notification = new Notification.Builder(this)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(msg)
                .SetSmallIcon(MicrosoftHouse.Droid.Resource.Drawable.icon)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(true)
                .Build();
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.Notify(0, notification);
        }


        protected override void OnError(Context context, string errorId)
        {
            Log.Error("GcmService", $"ERROR: {errorId}");
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Info("GcmService", $"Registered: {registrationId}");
            GcmService.RegistrationID = registrationId;
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Info("GcmService", $"Unregistered device from FCM");
            GcmService.RegistrationID = null;
        }
    }
}