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

		protected override void OnAppearing()
		{
			base.OnAppearing();
			(Application.Current.MainPage as MasterDetailPage).IsGestureEnabled = false;
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			(Application.Current.MainPage as MasterDetailPage).IsGestureEnabled = true;
		}

		public void OnDatePicker(object sender, EventArgs e)
		{
			datePicker.Focus();
		}

	}
}
