using System;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;

namespace MicrosoftHouse
{
	public class Reservation : TableData
	{
		public string User { get; set; }
		public string RoomName { get; set; }
		public DateTime Date  { get; set; }
		public TimeSpan StartingTime { get; set; }
		public TimeSpan EndingTime { get; set; }
		
	}
}
