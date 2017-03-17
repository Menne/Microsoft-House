using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class AllRooms
	{
		public AllRooms()
		{
		}

		public string Name { private set; get; }

		public string Piano { private set; get; }

		public Color Available { private set; get; }

		static AllRooms()
		{
			List <AllRooms> all = new List <AllRooms>();

			AllRooms room = new AllRooms
			{
				Name = "CIAO",
				Piano = "Piano 1",
				Available = Color.Green
			};

			all.Add(room);
			All = all;
		}

		public static IList<AllRooms> All { private set; get; }

	}
}
