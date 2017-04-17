using System;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using MicrosoftHouse.Models;

namespace MicrosoftHouse.Services
{
	public class AzureCloudService : ICloudService
	{
		/// <summary>
		/// The Client reference to the Azure Mobile App
		/// </summary>
		private MobileServiceClient Client { get; set; }

		public AzureCloudService()
		{
			Client = new MobileServiceClient(Locations.AppServiceUrl);
		}

		List<AppServiceIdentity> identities = null;

		public async Task<AppServiceIdentity> GetIdentityAsync()
		{
			if (Client.CurrentUser == null || Client.CurrentUser?.MobileServiceAuthenticationToken == null)
			{
				throw new InvalidOperationException("Not Authenticated");
			}

			if (identities == null)
			{
				identities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
			}

			if (identities.Count > 0)
				return identities[0];
			return null;
		}

		/// <summary>
		/// Returns a link to the specific table.
		/// </summary>
		/// <typeparam name="T">The model</typeparam>
		/// <returns>The table reference</returns>
		public async Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData
		{
			await InitializeAsync();
			return new AzureCloudTable<T>(Client);   
		}
		
		public Task LoginAsync()
		{
			var loginProvider = DependencyService.Get<ILoginProvider>();
			return loginProvider.LoginAsync(Client);

		}

		/*public Task LoginAsync(User user)
		{
			return client.LoginAsync("custom", JObject.FromObject(user)   }

		}*/

		public async Task LogoutAsync()
		{
			/*if (client.CurrentUser == null || client.CurrentUser.MobileServiceAuthenticationToken == null)
				return;

			// Log out of the identity provider (if required)

			// Invalidate the token on the mobile backend
			var authUri = new Uri($"{client.MobileAppUri}/.auth/logout");
			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Add("X-ZUMO-AUTH", client.CurrentUser.MobileServiceAuthenticationToken);
				await httpClient.GetAsync(authUri);
			}*/

			// Remove the token from the cache
			//DependencyService.Get<ILoginProvider>().RemoveTokenFromSecureStore();

			// Remove the token from the MobileServiceClient
			await Client.LogoutAsync();
		 }

		private async Task InitializeAsync()
		{
			// Short circuit - local database is already initialized
			if (Client.SyncContext.IsInitialized)
			{
				Debug.WriteLine("InitializeAsync: Short Circuit");
				return;
			}

			// Create a reference to the local sqlite store
			Debug.WriteLine("InitializeAsync: Initializing store");
			var store = new MobileServiceSQLiteStore("offlinecache.db");

			// Define the database schema - When defined you have to refresh it in order to modify it
			Debug.WriteLine("InitializeAsync: Defining Datastore");
			store.DefineTable<Event>();
			store.DefineTable<Room>();
			store.DefineTable<EventLocation>();
			store.DefineTable<CarPark>();
			store.DefineTable<Reservation>();


			// Actually create the store and update the schema
			Debug.WriteLine("InitializeAsync: Initializing SyncContext");
			await Client.SyncContext.InitializeAsync(store);

			// Do the sync
			Debug.WriteLine("InitializeAsync: Syncing Offline Cache");
			await SyncOfflineCacheAsync();
		}

		public async Task SyncOfflineCacheAsync()
		{
			Debug.WriteLine("SyncOfflineCacheAsync: Initializing...");
			await InitializeAsync();

			// Push the Operations Queue to the mobile backend
			Debug.WriteLine("SyncOfflineCacheAsync: Pushing Changes");
			await Client.SyncContext.PushAsync();

			// Pull each sync table
			Debug.WriteLine("SyncOfflineCacheAsync: Pulling tags event");
			var eventtable = await GetTableAsync<Event>(); 
			await eventtable.PullAsync();

			Debug.WriteLine("SyncOfflineCacheAsync: Pulling tasks room");
			var roomTable = await GetTableAsync<Room>(); 
			await roomTable.PullAsync();

			Debug.WriteLine("SyncOfflineCacheAsync: Pulling tasks locations");
			var locationTable = await GetTableAsync<EventLocation>(); 
			await locationTable.PullAsync();

			Debug.WriteLine("SyncOfflineCacheAsync: Pulling tasks park");
			var parkTable = await GetTableAsync<CarPark>(); 
			await parkTable.PullAsync();

			Debug.WriteLine("SyncOfflineCacheAsync: Pulling tasks reservation");
			var reservationTable = await GetTableAsync<Reservation>(); 
			await reservationTable.PullAsync();
		}

	}
}
