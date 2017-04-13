using System;
using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
	public class User : EntityData
	{
		public string Name { get; set; }

		public string Surname { get; set; }
		
	}
}
