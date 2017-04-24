using System;
using System.Threading.Tasks;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.Droid;
using Gcm.Client;
using TaskList.Droid.Services;
using System.Diagnostics;
using System.Net.Http;
using System.Collections.Generic;
using Android.Util;

[assembly: Xamarin.Forms.Dependency(typeof(DroidPlatformProvider))]
namespace MicrosoftHouse.Droid
{
	public class DroidPlatformProvider : IPlatformProvider
	{
        public Context RootView { get; private set; }

        public string GetSyncStore()
		{
			throw new NotImplementedException();
		}

		public void Init(Context context)
		{
            RootView = context;

            try
            {
                // Check to see if this client has the right permissions
                GcmClient.CheckDevice(RootView);
                GcmClient.CheckManifest(RootView);

                // Register for push
                GcmClient.Register(RootView, GcmHandler.SenderId);
                Debug.WriteLine($"GcmClient: Registered for push with FCM: {GcmClient.GetRegistrationId(RootView)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GcmClient: Cannot register for push: {ex.Message}");
            }

        }

        public async Task LoginAsync(MobileServiceClient client)
		{
			await client.LoginAsync(RootView, "microsoftaccount");
		}

        public async Task RegisterForPushNotifications(MobileServiceClient client)
        {
            if (GcmClient.IsRegistered(RootView))
            {
                try
                {
                    var registrationId = GcmClient.GetRegistrationId(RootView);
                    //var push = client.GetPush();
                    //await push.RegisterAsync(registrationId);

                    var installation = new DeviceInstallation
                    {
                        InstallationId = client.InstallationId,
                        Platform = "gcm",
                        PushChannel = registrationId
                    };
                    // Set up tags to request
                    installation.Tags.Add("topic:Sports");
                    // Set up templates to request
                    PushTemplate genericTemplate = new PushTemplate
                    {
                        Body = @"{""data"":{""message"":""$(message)""}}"
                    };
                    installation.Templates.Add("genericTemplate", genericTemplate);

                    // Register with NH
                    var response = await client.InvokeApiAsync<DeviceInstallation, DeviceInstallation>(
                        $"/push/installations/{client.InstallationId}",
                        installation,
                        HttpMethod.Put,
                        new Dictionary<string, string>());
                }
                catch (Exception ex)
                {
                    Log.Error("DroidPlatformProvider", $"Could not register with NH: {ex.Message}");
                }
            }
            else
            {
                Log.Error("DroidPlatformProvider", $"Not registered with FCM");
            }
        }
    }
}
