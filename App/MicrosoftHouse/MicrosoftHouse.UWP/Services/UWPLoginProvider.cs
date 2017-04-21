using MicrosoftHouse;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TaskList.UWP.Services;
using System;
using Windows.Networking.PushNotifications;
using System.Net.Http;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(UWPLoginProvider))]
namespace TaskList.UWP.Services
{
    public class UWPLoginProvider : ILoginProvider
    {
        public static PushNotificationChannel Channel { get; set; } = null;

        public async Task LoginAsync(MobileServiceClient client)
        {
            await client.LoginAsync("facebook");
        }

        public string GetSyncStore()
        {
            throw new NotImplementedException();
        }

        public async Task RegisterForPushNotifications(MobileServiceClient client)
        {
            if (UWPLoginProvider.Channel != null)
            {
                try
                {
                    var registrationId = UWPLoginProvider.Channel.Uri.ToString();
                    var installation = new DeviceInstallation
                    {
                        InstallationId = client.InstallationId,
                        Platform = "wns",
                        PushChannel = registrationId
                    };
                    // Set up tags to request
                    installation.Tags.Add("topic:Sports");
                    // Set up templates to request
                    var genericTemplate = new WindowsPushTemplate
                    {
                        Body = @"<toast><visual><binding template=""genericTemplate""><text id=""1"">$(message)</text></binding></visual></toast>"
                    };
                    genericTemplate.Headers.Add("X-WNS-Type", "wns/toast");

                    installation.Templates.Add("genericTemplate", genericTemplate);
                    // Register with NH
                    var recordedInstallation = await client.InvokeApiAsync<DeviceInstallation, DeviceInstallation>(
                        $"/push/installations/{client.InstallationId}",
                        installation,
                        HttpMethod.Put,
                        new Dictionary<string, string>());
                    System.Diagnostics.Debug.WriteLine("Completed NH Push Installation");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Fail($"[UWPPlatformProvider]: Could not register with NH: {ex.Message}");
                }
            }
        }
    }
}