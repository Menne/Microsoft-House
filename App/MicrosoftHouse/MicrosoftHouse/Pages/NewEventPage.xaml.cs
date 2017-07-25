using System;
using System.Collections.Generic;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class NewEventPage : ContentPage
	{
		public NewEventPage()
		{
			InitializeComponent();
			BindingContext = new NewEventViewModel();

            datePicker.MinimumDate = DateTime.Now;
        }

		// Edit page
		public NewEventPage(Event selectedEvent = null)
		{
			InitializeComponent();
			Title = "Edit Event";
			newEventButton.Text = "SAVE";
			BindingContext = new NewEventViewModel(selectedEvent);
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
		public void OnTimePickerFirst(object sender, EventArgs e)
		{
			timePickerFirst.Focus();
		}

		public void OnTimePickerSecond(object sender, EventArgs e)
		{
			timePickerSecond.Focus();
		}

		public void OnDatePicker(object sender, EventArgs e)
		{
			datePicker.Focus();
		}

		public void OnLocationPicker(object sender, EventArgs e)
		{
			locationPicker.Focus();
		}
	}
}
