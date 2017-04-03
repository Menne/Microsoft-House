using System;
using System.Collections.Generic;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class SelectedRoomPage : ContentPage
	{
		public SelectedRoomPage(Room room = null)
		{
			InitializeComponent();
			BindingContext = new SelectedRoomViewModel(room);
		}
	}
}
