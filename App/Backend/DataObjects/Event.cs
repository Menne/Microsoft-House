using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure.Mobile.Server;

namespace Backend
{
	public class Event : EntityData
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public TimeSpan StartingTime { get; set; }
		public TimeSpan EndingTime { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
	}
}
