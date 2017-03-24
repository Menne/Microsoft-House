using System;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class NewEventViewModel : ViewModelBase
	{
		string eventName, description;
		bool notifica;
        DateTime startingDateTime;

        public bool NonEmptyEventName;

        public NewEventViewModel()
		{
            
		}

		public string EventName
		{
			set { SetProperty(ref eventName, value); }
			get { return eventName; }
		}

		public string Description
		{
			set { SetProperty(ref description, value); }
			get { return description; }
		}



        public DateTime StartingDateTime
        {
            set { SetProperty(ref startingDateTime, value); }
            get { return startingDateTime; }
        }

        public bool Notifica
		{
			set { SetProperty(ref notifica, value); }
			get { return notifica; }
		}


        public void OnButtonClicked(object sender, EventArgs args)
        {
            // crea l'evento
        }

    }
}
