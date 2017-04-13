using System;
using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
	public class Reservation : EntityData
	{
		
		public User UserId { get; set; }

		public Room RoomId { get; set; }

		public DateTime Date { get; set; }

		public TimeSpan StartingTime { get; set; }

		public TimeSpan EndingTime { get; set; }
	}
}
