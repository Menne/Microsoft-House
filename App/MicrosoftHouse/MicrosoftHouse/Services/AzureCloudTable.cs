using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MicrosoftHouse.Abstractions;

namespace MicrosoftHouse.Services
{
	public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
	{
		IMobileServiceSyncTable<T> table;

		public AzureCloudTable(MobileServiceClient client)
		{
			table = client.GetSyncTable<T>();
		}

		// CREATE
		public async Task<T> CreateEventAsync(T newEvent)
		{
			await table.InsertAsync(newEvent);
			return newEvent;
		}

		public Task<T> CreateEventLocationAsync(T location)
		{
			throw new NotImplementedException();
		}

		public async Task<T> CreateParkAsync(T slot)
		{
            await table.InsertAsync(slot);
            return slot;
        }

		public async Task<T> CreateReservationAsync(T reservation)
		{
			await table.InsertAsync(reservation);
			return reservation;
		}

		public async Task<T> CreateRoomAsync(T room)
		{
			await table.InsertAsync(room);
			return room;
		}

		// DELETE
		public async Task DeleteEventAsync(T currentEvent)
		{
			await table.DeleteAsync(currentEvent);
		}

		public Task DeleteEventLocationAsync(T location)
		{
			throw new NotImplementedException();
		}

		public async Task DeleteParkAsync(T slot)
		{
            await table.DeleteAsync(slot);
        }

		public async Task DeleteReservationAsync(T reservation)
		{
			await table.DeleteAsync(reservation);
		}

		public async Task DeleteRoomAsync(T room)
		{
			await table.DeleteAsync(room);
		}




		// READ ALL
		public async Task<ICollection<T>> ReadAllEventLocationsAsync()
		{
			return await table.ToListAsync();
		}

		public async Task<ICollection<T>> ReadAllEventsAsync()
		{
			return await table.ToListAsync();
		}

		public async Task<ICollection<T>> ReadAllParksAsync()
		{
			return await table.ToListAsync();
		}

		public async Task<ICollection<T>> ReadAllReservationsAsync()
		{
			return await table.ToListAsync();
		}

		public async Task<ICollection<T>> ReadAllRoomsAsync()
		{
			return await table.ToListAsync();
		}


		// READ
		public async Task<T> ReadEventAsync(string id)
		{
			return await table.LookupAsync(id);
		}

		public async Task<T> ReadEventLocationAsync(string id)
		{
			return await table.LookupAsync(id);
		}

		public async Task<T> ReadParkAsync(string id)
		{
			return await table.LookupAsync(id);
		}

		public async Task<T> ReadReservationAsync(string id)
		{
			return await table.LookupAsync(id);
		}

		public async Task<T> ReadRoomAsync(string id)
		{
			return await table.LookupAsync(id);
		}

		// UPDATE
		public async Task<T> UpdateEventAsync(T currentEvent)
		{
			await table.UpdateAsync(currentEvent);
			return currentEvent;
		}

		public Task<T> UpdateEventLocationAsync(T room)
		{
			throw new NotImplementedException();
		}

		public async Task<T> UpdateParkAsync(T slot)
		{
			await table.UpdateAsync(slot);
			return slot;
		}

		public async Task<T> UpdateReservationAsync(T reservation)
		{
			await table.UpdateAsync(reservation);
			return reservation;
		}

		public async Task<T> UpdateRoomAsync(T room)
		{
			await table.UpdateAsync(room);
			return room;
		}



		// Synch

		public async Task PullAsync()
		{
			string queryName = $"incsync_{typeof(T).Name}";
			await table.PullAsync(queryName, table.CreateQuery());
		}
	}
}
