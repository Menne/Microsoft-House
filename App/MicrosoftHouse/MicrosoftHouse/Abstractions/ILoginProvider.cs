using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace MicrosoftHouse
{
	public interface ILoginProvider
	{
		Task LoginAsync(MobileServiceClient client);

		//MobileServiceUser RetrieveTokenFromSecureStore();

		//void StoreTokenInSecureStore(MobileServiceUser user);

		//void RemoveTokenFromSecureStore();

		Task RegisterForPushNotifications(MobileServiceClient client);

		string GetSyncStore();
	}
}
