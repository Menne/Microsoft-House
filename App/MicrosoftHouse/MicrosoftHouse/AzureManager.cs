using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace MicrosoftHouse
{
	public class AzureManager
	{
		IMobileServiceTable<Room> roomTable;
		MobileServiceClient client;

		public AzureManager()
		{

			client = new MobileServiceClient("https://microsofthouse.azurewebsites.net");
			roomTable = client.GetTable<Room>();

		}

		public MobileServiceClient CurrentClient
		{
			get { return client; }
		}


		public async Task SaveTaskAsync(Room room)
		{
			await roomTable.InsertAsync(room);
		}
	}
}
