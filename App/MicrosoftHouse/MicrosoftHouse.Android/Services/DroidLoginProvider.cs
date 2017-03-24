using System.Threading.Tasks;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DroidLoginProvider))]
namespace MicrosoftHouse.Droid.Services
{
	public class DroidLoginProvider : ILoginProvider
	{
		Context context;

		public void Init(Context context)
		{
			this.context = context;
		}

		public async Task LoginAsync(MobileServiceClient client)
		{
			await client.LoginAsync(context, "facebook");
		}
	}
}
