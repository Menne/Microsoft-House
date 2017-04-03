using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage()
		{
			InitializeComponent();
			BindingContext = new ProfileViewModel();
		}
	}
}
