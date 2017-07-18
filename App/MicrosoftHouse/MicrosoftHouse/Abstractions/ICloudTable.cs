using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicrosoftHouse.Abstractions
{
	public interface ICloudTable<T> where T : TableData
	{
		//ROOMS
		Task<T> CreateRoomAsync(T room);
		Task<T> ReadRoomAsync(string id);
		Task<T> UpdateRoomAsync(T room);
		Task DeleteRoomAsync(T room);
		Task<ICollection<T>> ReadAllRoomsAsync();

		//EVENTS
		Task<T> CreateEventAsync(T newEvent);
		Task<T> ReadEventAsync(string id);
		Task<T> UpdateEventAsync(T currentEvent);
		Task DeleteEventAsync(T currentEvent);
		Task<ICollection<T>> ReadAllEventsAsync();

		//EVENTLOCATIONS
		Task<T> CreateEventLocationAsync(T location);
		Task<T> ReadEventLocationAsync(string id); // -- Utilizzato
		Task<T> UpdateEventLocationAsync(T location);
		Task DeleteEventLocationAsync(T location);
		Task<ICollection<T>> ReadAllEventLocationsAsync();

		//RESERVATION
		Task<T> CreateReservationAsync(T reservation);
		Task<T> ReadReservationAsync(string id);
		Task<T> UpdateReservationAsync(T reservation);
		Task DeleteReservationAsync(T reservation);
		Task<ICollection<T>> ReadAllReservationsAsync();


		//PARK
		Task<T> CreateParkAsync(T slot);
		Task<T> ReadParkAsync(string id); 
		Task<T> UpdateParkAsync(T slot); // -- Utilizzato
		Task DeleteParkAsync(T slot);
		Task<ICollection<T>> ReadAllParksAsync();

		Task PullAsync();
	}
}
