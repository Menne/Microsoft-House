using System;
using MicrosoftHouse.Abstractions;

namespace MicrosoftHouse.Models
{
	public class Room : TableData
	{
		public string Name { get; set; }

		public int Floor { get; set; }

		public int Seats { get; set; }
	}
}
