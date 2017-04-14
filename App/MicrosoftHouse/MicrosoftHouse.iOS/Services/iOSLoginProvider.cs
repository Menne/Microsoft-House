using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.iOS;
using Newtonsoft.Json.Linq;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSLoginProvider))]
namespace MicrosoftHouse.iOS
{
	public class iOSLoginProvider : ILoginProvider
	{
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
			var rootView = UIApplication.SharedApplication.KeyWindow.RootViewController;

			//FACEBOOK

			await client.LoginAsync(RootView, "facebook");

			// Client Flow
			/*var accessToken = await LoginADALAsync(rootView);
			var zumoPayload = new JObject();
			zumoPayload["access_token"] = accessToken;
			await client.LoginAsync("aad", zumoPayload);*/

			// Server Flow
			//await client.LoginAsync(rootView, "aad");   
		}

		public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;
	}
}

