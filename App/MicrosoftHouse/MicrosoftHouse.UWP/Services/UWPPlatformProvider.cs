using MicrosoftHouse;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TaskList.UWP.Services;
using System;
using Windows.Networking.PushNotifications;
using System.Net.Http;
using System.Collections.Generic;
using Windows.Security.Credentials;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(UWPPlatformProvider))]
namespace TaskList.UWP.Services
{
    public class UWPPlatformProvider : IPlatformProvider
    {
        public PasswordVault PasswordVault { get; private set; }
        public static PushNotificationChannel Channel { get; set; } = null;
        
        public UWPPlatformProvider()
        {
            PasswordVault = new PasswordVault();
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client)
        {
            // Server-Flow Version
            return await client.LoginAsync("microsoftaccount");
        }

        public MobileServiceUser RetrieveTokenFromSecureStore()
        {
            try
            {
                // Check if the token is available within the password vault
                var acct = PasswordVault.FindAllByResource("tasklist").FirstOrDefault();
                if (acct != null)
                {
                    var token = PasswordVault.Retrieve("tasklist", acct.UserName).Password;
                    if (token != null && token.Length > 0)
                    {
                        return new MobileServiceUser(acct.UserName)
                        {
                            MobileServiceAuthenticationToken = token
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving existing token: {ex.Message}");
            }
            return null;
        }

        public void StoreTokenInSecureStore(MobileServiceUser user)
        {
            PasswordVault.Add(new PasswordCredential("tasklist", user.UserId, user.MobileServiceAuthenticationToken));
        }

        public void RemoveTokenFromSecureStore()
        {
            try
            {
                // Check if the token is available within the password vault
                var acct = PasswordVault.FindAllByResource("tasklist").FirstOrDefault();
                if (acct != null)
                {
                    PasswordVault.Remove(acct);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving existing token: {ex.Message}");
            }
        }


        public async Task RegisterForPushNotifications(MobileServiceClient client)
        {
            if (UWPPlatformProvider.Channel != null)
            {
                try
                {
                    var registrationId = UWPPlatformProvider.Channel.Uri.ToString();
                    var installation = new DeviceInstallation
                    {
                        InstallationId = client.InstallationId,
                        Platform = "wns",
                        PushChannel = registrationId
                    };
                    // Set up tags to request
                    installation.Tags.Add("topic:Events");
                    // Set up templates to request
                    var genericTemplate = new WindowsPushTemplate
                    {
                        Body = @"<toast>
                                    <visual>
                                        <binding template=""ToastText01"">
                                            <text id=""1"">Microsoft House</text>
                                            <text id=""1"">$(message)</text>
                                        </binding>
                                    </visual>
                                </toast>"
                    };
                    genericTemplate.Headers.Add("X-WNS-Type", "wns/toast");

                    installation.Templates.Add("ToastText01", genericTemplate);
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
        

        public string GetSyncStore()
        {
            throw new NotImplementedException();
        }
    }
}