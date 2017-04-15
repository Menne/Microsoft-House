using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
	public class Reservation : EntityData
	{
		public string User { get; set; }
		public string RoomName { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan StartingTime { get; set; }
		public TimeSpan EndingTime { get; set; }
	}
}
