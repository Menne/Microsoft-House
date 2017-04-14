using System;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace MicrosoftHouse.Services
{
	public class AzureCloudService : ICloudService
	{
		MobileServiceClient client;

		public AzureCloudService()
		{
			client = new MobileServiceClient(Locations.AppServiceUrl);
		}

		List<AppServiceIdentity> identities = null;

		public async Task<AppServiceIdentity> GetIdentityAsync()
		{
			if (client.CurrentUser == null || client.CurrentUser?.MobileServiceAuthenticationToken == null)
			{
				throw new InvalidOperationException("Not Authenticated");
			}

			if (identities == null)
			{
				identities = await client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
			}

			if (identities.Count > 0)
				return identities[0];
			return null;
		}

		public ICloudTable<T> GetTable<T>() where T : TableData => new AzureCloudTable<T>(client);
		
		public Task LoginAsync()
		{
			var loginProvider = DependencyService.Get<ILoginProvider>();
			return loginProvider.LoginAsync(client);

		}

		/*public Task LoginAsync(User user)
		{
			return client.LoginAsync("custom", JObject.FromObject(user)   }

		}*/
	}
}
