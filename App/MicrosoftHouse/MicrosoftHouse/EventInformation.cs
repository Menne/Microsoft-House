using System;
namespace MicrosoftHouse
{
	public class EventInformation : ViewModelBase
	{
		string eventName, description;
		bool notifica;

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

		public bool Notifica
		{
			set { SetProperty(ref notifica, value); }
			get { return notifica; }
		}


	}
}
