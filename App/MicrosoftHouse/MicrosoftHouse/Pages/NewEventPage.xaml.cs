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
	}
}
