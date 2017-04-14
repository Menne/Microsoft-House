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
			//timePickerFirst = DateTime.No;

			BindingContext = new NewEventViewModel();
		}

		// PAGINA PER L'EDIT
		public NewEventPage(Event selectedEvent = null)
		{
			InitializeComponent();
			Title = "Edit Event";
			newEventButton.Text = "EDIT";
			BindingContext = new NewEventViewModel(selectedEvent);
			//eventName.Text = selectedEvent.Name;
			//eventDescription.Text = selectedEvent.Description;
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
