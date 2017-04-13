using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicrosoftHouse.Abstractions
{
	public interface ICloudTable<T> where T : TableData
	{
		//ROOMS
		Task<T> CreateRoomAsynch(T room);
		Task<T> ReadRoomAsync(string id);
		Task<T> UpdateRoomAsync(T room);
		Task DeleteRoomAsync(T room);
		Task<ICollection<T>> ReadAllRoomsAsync();

		//EVENTS
		Task<T> CreateEventAsynch(T room);
		Task<T> ReadEventAsync(string id);
		Task<T> UpdateEventAsync(T room);
		Task DeleteEventAsync(T room);
		Task<ICollection<T>> ReadAllEventsAsync();

		//EVENTLOCATIONS
		Task<T> CreateEventLocationAsynch(T room);
		Task<T> ReadEventLocationAsync(string id); // -- Utilizzato
		Task<T> UpdateEventLocationAsync(T room);
		Task DeleteEventLocationAsync(T room);
		Task<ICollection<T>> ReadAllEventLocationsAsync();

		//RESERVATION
		Task<T> CreateReservationAsynch(T room);
		Task<T> ReadReservationAsync(string id);
		Task<T> UpdateReservationAsync(T room);
		Task DeleteReservationAsync(T room);
		Task<ICollection<T>> ReadAllReservationsAsync();


		//PARK
		Task<T> CreateParkAsynch(T room);
		Task<T> ReadParkAsync(string id); 
		Task<T> UpdateParkAsync(T room); // -- Utilizzato
		Task DeleteParkAsync(T room);
		Task<ICollection<T>> ReadAllParksAsync();
	}
}
