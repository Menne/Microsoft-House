using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class NewRoomList : ContentPage
	{
		public NewRoomList()
		{
			InitializeComponent();

		}

		async void OnNewRoom(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewRoomPage());
		}
	}
}
