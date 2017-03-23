using System;
using MicrosoftHouse.Abstractions;

namespace MicrosoftHouse.Models
{
	public class Room : TableData
	{
		public string Name { get; set; }

		public string Floor { get; set; }

		public string Seats { get; set; }
	}
}
