using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class SearchRoomPage : ContentPage
	{
		public SearchRoomPage()
		{
			InitializeComponent();
			BindingContext = new SearchRoomViewModel();
		}

	}
}
