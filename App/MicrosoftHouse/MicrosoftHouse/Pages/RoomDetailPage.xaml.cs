using System;
using System.Collections.Generic;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class RoomDetailPage : ContentPage
	{
		public RoomDetailPage(Room room = null)
		{
			InitializeComponent();
			BindingContext = new RoomDetailViewModel();
		}
	}
}
