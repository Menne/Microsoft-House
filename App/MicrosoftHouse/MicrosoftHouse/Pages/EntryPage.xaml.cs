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
            InitializeComponent();
			BindingContext = new EntryPageViewModel();
		}

	}
}
