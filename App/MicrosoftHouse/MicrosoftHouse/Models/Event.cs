using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;

namespace MicrosoftHouse.Models
{
	public class Event : TableData
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public TimeSpan StartingTime { get; set; }
		public TimeSpan EndingTime { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }

    }
}
