using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicrosoftHouse.Abstractions
{
	public interface ICloudTable<T> where T : TableData
	{
		Task<T> CreateRoomAsynch(T room);
		Task<T> ReadRoomAsync(string id);
		Task<T> UpdateRoomAsync(T room);
		Task DeleteRoomAsync(T room);

		Task<ICollection<T>> ReadAllRoomsAsync();
	}
}
