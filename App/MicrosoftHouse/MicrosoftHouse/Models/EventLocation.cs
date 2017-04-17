using System;
using MicrosoftHouse.Abstractions;

namespace MicrosoftHouse
{
	public class EventLocation : TableData
	{
		public string Name { get; set; }
		public string Floor { get; set; }
		public string Seats { get; set; }
	}
}
