﻿using System;
using Microsoft.Azure.Mobile.Server;

namespace Backend
{
	public class Event : EntityData
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Place { get; set; }
		//public DateTime StartingDate { get; set; }
		//public DateTime EndingDate { get; set; }
		public TimeSpan StartingTime { get; set; }
		public TimeSpan EndingTime { get; set; }
		public DateTime Date { get; set; }
		public EventLocation Location { get; set; }
	}
}