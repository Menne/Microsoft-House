using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.iOS;
using Newtonsoft.Json.Linq;
using UIKit;
using Xamarin.Auth;

[assembly: Xamarin.Forms.Dependency(typeof(iOSPlatformProvider))]
namespace MicrosoftHouse.iOS
{
	public class iOSPlatformProvider : IPlatformProvider
	{
        public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;

        public AccountStore AccountStore { get; private set; }
		
		public iOSPlatformProvider()
		{
			AccountStore = AccountStore.Create();   
		}

        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client)
        {
            // Server-Flow Version
            return await client.LoginAsync(RootView, "microsoftaccount");
        }

        public MobileServiceUser RetrieveTokenFromSecureStore()
        {
            var accounts = AccountStore.FindAccountsForService("tasklist");
            if (accounts != null)
            {
                foreach (var acct in accounts)
                {
                    string token;

                    if (acct.Properties.TryGetValue("token", out token))
                    {
                        return new MobileServiceUser(acct.Username)
                        {
                            MobileServiceAuthenticationToken = token
                        };
                    }
                }
            }
            return null;
        }

        public void StoreTokenInSecureStore(MobileServiceUser user)
        {
            var account = new Account(user.UserId);
            account.Properties.Add("token", user.MobileServiceAuthenticationToken);
            AccountStore.Save(account, "tasklist");
        }

        public void RemoveTokenFromSecureStore()
        {
            var accounts = AccountStore.FindAccountsForService("tasklist");
            if (accounts != null)
            {
                foreach (var acct in accounts)
                {
                    AccountStore.Delete(acct, "tasklist");
                }
            }
        }


        public Task RegisterForPushNotifications(MobileServiceClient client)
        {
            throw new NotImplementedException();
        }


        public string GetSyncStore()
        {
            return "syncstore.db";
        }
    }
}

