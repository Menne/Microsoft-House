using System;
using System.Collections.Generic;
using MicrosoftHouse.ViewModels;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class NewEvent : ContentPage
	{
		public NewEvent()
		{
			InitializeComponent();
            BindingContext = new NewEventViewModel();
            
        }


        public void OnSwitchToggled(object sender, EventArgs args)
        {
            // deve diventare behavior?
        }
    }
}
