using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class NewEvent : ContentPage
	{
        public bool NonEmptyEventName;

		public NewEvent()
		{
			InitializeComponent();
		}

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = (Entry)sender;
            NonEmptyEventName = CheckIfEmptyEventName(entry.Text);

        }

        private bool CheckIfEmptyEventName(String eventName)
        {
            if (String.IsNullOrEmpty(eventName))
                return false;
            else
                return true;
        }

        public void OnButtonClicked(object sender, EventArgs args)
        {
            EventInformation newEvent = new EventInformation();
            // popola l'evento
        }

    }
}
