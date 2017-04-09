using System;
using System.Collections.Generic;
using MicrosoftHouse.Models;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class SelectedEventPage : ContentPage
	{
		public SelectedEventPage(Event selectedEvent = null)
		{
			InitializeComponent();
			BindingContext = new SelectedEventPageViewModel(selectedEvent);
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
