using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class RoomsNavPage : ContentPage
	{
		public RoomsNavPage()
		{
			InitializeComponent();
			BindingContext = new RoomViewModel();
		}

		public ListView ListView { get { return listView; } }


		public void OnPressedAV(object sender, EventArgs e)
		{
			allRoomsButton.BackgroundColor = Color.Transparent;
			allRoomsButton.TextColor = Color.White;
			reservedRoomsButton.TextColor = Color.White;
			reservedRoomsButton.BackgroundColor = Color.Transparent;

			availableRoomsButton.TextColor = Color.FromHex("#FF01A4EF");
			availableRoomsButton.BackgroundColor = Color.White;

		}

		public void OnPressedALL(object sender, EventArgs e)
		{
			availableRoomsButton.BackgroundColor = Color.Transparent;
			availableRoomsButton.TextColor = Color.White;
			reservedRoomsButton.TextColor = Color.White;
			reservedRoomsButton.BackgroundColor = Color.Transparent;

			allRoomsButton.TextColor = Color.FromHex("#FF01A4EF");
			allRoomsButton.BackgroundColor = Color.White;
		}

		public void OnPressedRE(object sender, EventArgs e)
		{
			availableRoomsButton.BackgroundColor = Color.Transparent;
			availableRoomsButton.TextColor = Color.White;
			allRoomsButton.TextColor = Color.White;
			allRoomsButton.BackgroundColor = Color.Transparent;

			reservedRoomsButton.TextColor = Color.FromHex("#FF01A4EF");
			reservedRoomsButton.BackgroundColor = Color.White;

		}

	}
}
