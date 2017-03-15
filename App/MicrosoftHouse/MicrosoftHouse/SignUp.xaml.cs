using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class SignUp : ContentPage
	{
		public SignUp()
		{
			InitializeComponent();
		}

		void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			//Entry entry = (Entry)sender;
			//if mail is not valid
			//entry.TextColor = Color.Red;
			//signInButton.isEnabled = false;
		}

		void OnEntryCompleted(object sender, EventArgs args)
		{
			
		}
	}
}
