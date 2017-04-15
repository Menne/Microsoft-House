using System;
using Microsoft.Azure.Mobile.Server;

namespace Backend
{
	public class EventLocation : EntityData
	{
		public string Name { get; set; }

		public string Floor { get; set; }

		public string Seats { get; set; }
	}
}
