using System;
namespace MicrosoftHouse
{
	public class EventInformation : ViewModelBase
	{
		string eventName, description;
		bool notifica;
        DateTime dateAndTime;

        //Notifiche, data, ora...

        public EventInformation()
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

        public DateTime DateAndTime
        {
            set { SetProperty(ref dateAndTime, value); }
            get { return dateAndTime; }
        }

        public bool Notifica
		{
			set { SetProperty(ref notifica, value); }
			get { return notifica; }
		}

        

    }
}
