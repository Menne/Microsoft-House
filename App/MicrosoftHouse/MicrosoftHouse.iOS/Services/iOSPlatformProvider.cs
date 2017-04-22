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


		//public AccountStore AccountStore { get; private set; }

		public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;

		/*public iOSLoginProvider()
		{
			//AccountStore = AccountStore.Create();   
		}*/

		/// <summary>
		/// Login via ADAL
		/// </summary>
		/// <returns>(async) token from the ADAL process</returns>
		/*public async Task<string> LoginADALAsync(UIViewController view)
		{
			Uri returnUri = new Uri(Locations.AadRedirectUri);

			var authContext = new AuthenticationContext(Locations.AadAuthority);
			if (authContext.TokenCache.ReadItems().Count() > 0)
			{
				authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
			}
			var authResult = await authContext.AcquireTokenAsync(
				Locations.AppServiceUrl, 
				Locations.AadClientId,   
				returnUri,               
				new PlatformParameters(view));
			return authResult.AccessToken;
		}*/

		public async Task LoginAsync(MobileServiceClient client)
		{
			// Facebook client flow with token

			//var accessToken = await LoginFacebookAsync();
			//var zumoPayload = new JObject();
			//zumoPayload["access_token"] = accessToken;
			//await client.LoginAsync("facebook", zumoPayload);



			//FACEBOOK
			await client.LoginAsync(RootView, "facebook");

			// Client Flow
			//var accessToken = await LoginADALAsync(rootView);
			//var zumoPayload = new JObject();
			//zumoPayload["access_token"] = accessToken;
			//await client.LoginAsync("aad", zumoPayload);

			// Server Flow
			//await client.LoginAsync(rootView, "aad");   
		}

		private TaskCompletionSource<string> fbtcs;



		/*public async Task<string> LoginFacebookAsync()
		{
			fbtcs = new TaskCompletionSource<string>();
			var loginManager = new LoginManager();

			loginManager.LogInWithReadPermissions(new[] { "public_profile" }, RootView, LoginTokenHandler);
			return await fbtcs.Task;
		}

		private void LoginTokenHandler(LoginManagerLoginResult loginResult, NSError error)
		{
			if (loginResult.Token != null)
			{
				fbtcs.TrySetResult(loginResult.Token.TokenString);
			}
			else
			{
				fbtcs.TrySetException(new Exception("Facebook Client Flow Login Failed"));
			}
		}*/



		/*public void StoreTokenInSecureStore(MobileServiceUser user)
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
		}*/

		public string GetSyncStore()
		{
			return "syncstore.db";   
		}

        public Task RegisterForPushNotifications(MobileServiceClient client)
        {
            throw new NotImplementedException();
        }
    }
}

