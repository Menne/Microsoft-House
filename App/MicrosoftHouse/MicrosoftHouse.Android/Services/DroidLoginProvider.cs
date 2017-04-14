using System.Threading.Tasks;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(DroidLoginProvider))]
namespace MicrosoftHouse.Droid
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
