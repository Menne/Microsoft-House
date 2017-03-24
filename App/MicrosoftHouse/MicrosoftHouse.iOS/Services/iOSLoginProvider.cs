using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.iOS.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSLoginProvider))]
namespace MicrosoftHouse.iOS.Services
{
	public class iOSLoginProvider : ILoginProvider
	{
		public async Task LoginAsync(MobileServiceClient client)
		{
			await client.LoginAsync(RootView, "facebook");
		}

		public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;
	}
}
