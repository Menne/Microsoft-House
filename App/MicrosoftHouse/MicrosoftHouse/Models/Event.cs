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

		//[Newtonsoft.Json.JsonProperty("Id")]
		//public string Id { get; set; }
		//[Microsoft.WindowsAzure.MobileServices.Version]
		//public string AzureVersion { get; set; }
		public string User { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public TimeSpan StartingTime { get; set; }
		public TimeSpan EndingTime { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }

    }
}
