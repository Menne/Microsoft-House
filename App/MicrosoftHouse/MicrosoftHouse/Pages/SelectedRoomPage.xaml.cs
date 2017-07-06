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

		public void OnStartingTimePicker(object sender, EventArgs e)
		{
			startingTimePicker.Focus();
		}

        public void OnEndingTimePicker(object sender, EventArgs e)
        {
            endingTimePicker.Focus();
        }

        public void OnDatePicker(object sender, EventArgs e)
		{
			datePicker.Focus();
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
	}
}
