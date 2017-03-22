using System;
using Microsoft.Azure.Mobile.Server;

namespace Backend
{
	public class Reservation : EntityData
	{
		public User UserId { get; set; }

		public Room RoomId { get; set; }

		public DateTime StartingTime { get; set; }

		public DateTime EndingTime { get; set; }

	}
}
