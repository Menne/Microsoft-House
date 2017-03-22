﻿using System;
using Microsoft.Azure.Mobile.Server;

namespace Backend
{
	public class Room : EntityData
	{
		public string Name { get; set; }

		public int Floor { get; set; }

		public bool Available { get; set; }
	}
}
