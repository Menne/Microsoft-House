using System;
using System.Collections.Generic;
using MicrosoftHouse.ViewModels;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class EntryPage : ContentPage
	{
		public EntryPage()
		{
            //Prova Visual Studio WIN
			InitializeComponent();
			BindingContext = new EntryPageViewModel();
		}

		async void OnHome(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PageIniziale());
		}
	}
}
