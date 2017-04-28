using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;

namespace MicrosoftHouse.Abstractions
{
	public interface ICloudService
	{
		Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData;

        Task<MobileServiceUser> LoginAsync();

        Task<AppServiceIdentity> GetIdentityAsync();

		Task LogoutAsync();

		Task SyncOfflineCacheAsync();

		Task RegisterForPushNotifications();
	}
}
