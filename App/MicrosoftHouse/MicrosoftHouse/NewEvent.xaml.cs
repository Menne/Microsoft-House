using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class NewEvent : ContentPage
	{

		public NewEvent()
		{
			InitializeComponent();
		}

        public void OnButtonClicked(object sender, EventArgs args)
        {
            EventInformation newEvent = new EventInformation();
            // popola l'evento
            //     newEvent.DateAndTime = DatePicker;

        }
    }
}
