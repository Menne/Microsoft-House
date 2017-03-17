using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class AllRoomsView : ContentPage
	{
		public AllRoomsView()
		{
			InitializeComponent();

			//BindingContext = new AllRoomsViewModel();
		}

		async void OnNewRoom(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewRoomPage());
		}
	}
}
