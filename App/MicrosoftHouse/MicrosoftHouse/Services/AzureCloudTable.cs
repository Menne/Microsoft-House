using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.Abstractions;

namespace MicrosoftHouse.Services
{
	public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
	{
		MobileServiceClient client;
		IMobileServiceTable<T> table;

		public AzureCloudTable(MobileServiceClient client)
		{
			this.client = client;
			this.table = client.GetTable<T>();
		}

		public async Task<T> CreateRoomAsynch(T room)
		{
			await table.InsertAsync(room);
			return room;
		}

		public async Task DeleteRoomAsync(T room)
		{
			await table.DeleteAsync(room);
		}

		public async Task<ICollection<T>> ReadAllRoomsAsync()
		{
			return await table.ToListAsync();
		}

		public async Task<T> ReadRoomAsync(string id)
		{
			return await table.LookupAsync(id);
		}

		public async Task<T> UpdateRoomAsync(T room)
		{
			await table.UpdateAsync(room);
			return room;
		}
	}
}
