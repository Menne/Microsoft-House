using System;
using MicrosoftHouse.Abstractions;

namespace MicrosoftHouse
{
	public class SettingsViewModel : BaseViewModel
	{
		public SettingsViewModel()
		{
		}

        Boolean notificationsOn;
        private Boolean NotificationsOn
        {
            get { return notificationsOn; }
            set { SetProperty(ref notificationsOn, value, "NotificationsOn"); }
        }
	}
}
