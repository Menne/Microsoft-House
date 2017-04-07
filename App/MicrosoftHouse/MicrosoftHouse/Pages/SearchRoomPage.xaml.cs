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

		public void OnTimePicker(object sender, EventArgs e)
		{
			timePicker.Focus();
		}

		public void OnDatePicker(object sender, EventArgs e)
		{
			datePicker.Focus();
		}

	}
}
