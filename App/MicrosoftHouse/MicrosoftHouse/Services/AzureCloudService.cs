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
using System.Text;
using TaskList.Helpers;
using System.Net.Http;

namespace MicrosoftHouse.Services
{
	public class AzureCloudService : ICloudService
	{
		/// <summary>
		/// The Client reference to the Azure Mobile App
		/// </summary>
		private MobileServiceClient client { get; set; }

        public AzureCloudService()
        {
            client = new MobileServiceClient(Locations.AppServiceUrl, new AuthenticationDelegatingHandler());

            if (Locations.AlternateLoginHost != null)
                client.AlternateLoginHost = new Uri(Locations.AlternateLoginHost);
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

        /// <summary>
        /// Returns a link to the specific table.
        /// </summary>
        /// <typeparam name="T">The model</typeparam>
        /// <returns>The table reference</returns>
        public async Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData
        {
            await InitializeAsync();
            return new AzureCloudTable<T>(client);
        }

        public async Task<MobileServiceUser> LoginAsync()
        {
            var loginProvider = DependencyService.Get<IPlatformProvider>();

            client.CurrentUser = loginProvider.RetrieveTokenFromSecureStore();
            if (client.CurrentUser != null)
            {
                // User has previously been authenticated - try to Refresh the token
                try
                {
                    var refreshed = await client.RefreshUserAsync();
                    if (refreshed != null)
                    {
                        loginProvider.StoreTokenInSecureStore(refreshed);
                        return refreshed;
                    }
                }
                catch (Exception refreshException)
                {
                    Debug.WriteLine($"Could not refresh token: {refreshException.Message}");
                }
            }

            if (client.CurrentUser != null && !IsTokenExpired(client.CurrentUser.MobileServiceAuthenticationToken))
            {
                // User has previously been authenticated, no refresh is required
                return client.CurrentUser;
            }

            // We need to ask for credentials at this point
            await loginProvider.LoginAsync(client);
            if (client.CurrentUser != null)
            {
                // We were able to successfully log in
                loginProvider.StoreTokenInSecureStore(client.CurrentUser);
            }
            return client.CurrentUser;
        }

        bool IsTokenExpired(string token)
        {
            // Get just the JWT part of the token (without the signature).
            var jwt = token.Split(new Char[] { '.' })[1];

            // Undo the URL encoding.
            jwt = jwt.Replace('-', '+').Replace('_', '/');
            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new ArgumentException("The token is not a valid Base64 string.");
            }

            // Convert to a JSON String
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Parse as JSON object and get the exp field value,
            // which is the expiration date as a JavaScript primative date.
            JObject jsonObj = JObject.Parse(jsonString);
            var exp = Convert.ToDouble(jsonObj["exp"].ToString());

            // Calculate the expiration by adding the exp value (in seconds) to the
            // base date of 1/1/1970.
            DateTime minTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expire = minTime.AddSeconds(exp);
            return (expire < DateTime.UtcNow);
        }


        public async Task LogoutAsync()
		{
            if (client.CurrentUser == null || client.CurrentUser.MobileServiceAuthenticationToken == null)
                return;

            // Log out of the identity provider (if required)

            // Invalidate the token on the mobile backend
            var authUri = new Uri($"{client.MobileAppUri}/.auth/logout");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-ZUMO-AUTH", client.CurrentUser.MobileServiceAuthenticationToken);
                await httpClient.GetAsync(authUri);
            }

            // Remove the token from the cache
            DependencyService.Get<IPlatformProvider>().RemoveTokenFromSecureStore();

            // Remove the token from the MobileServiceClient
            await client.LogoutAsync();
        }

        private async Task InitializeAsync()
		{
			// Short circuit - local database is already initialized
			if (client.SyncContext.IsInitialized)
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
			await client.SyncContext.InitializeAsync(store);

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
			await client.SyncContext.PushAsync();

			// Pull each sync table
			Debug.WriteLine("SyncOfflineCacheAsync: Pulling tasks event");
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

		public async Task RegisterForPushNotifications()
		{
			var platformProvider = DependencyService.Get<IPlatformProvider>();
			await platformProvider.RegisterForPushNotifications(client);   
		}

	}
}
