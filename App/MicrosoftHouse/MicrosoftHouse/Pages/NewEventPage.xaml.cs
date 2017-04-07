using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class NewEventPage : ContentPage
	{
		public NewEventPage()
		{
			InitializeComponent();

			BindingContext = new NewEventViewModel();
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
	}
}
