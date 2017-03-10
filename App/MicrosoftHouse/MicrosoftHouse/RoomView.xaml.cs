using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class RoomView : ContentView
	{
		bool available;
		string roomName;

		public RoomView()
		{
			InitializeComponent();
		}

		public string RoomName
		{
			set
			{
				roomName = value;
				roomLabel.Text = roomName;
			}
			get
			{
				return roomName;
			}
		}

		public bool Available
		{
			set
			{
				available = value;

				if (available)
					roomFrame.BackgroundColor = Color.Green;
				else
					roomFrame.BackgroundColor = Color.Red;

			}
			get
			{
				return available;
			}
			
		}

	}
}
