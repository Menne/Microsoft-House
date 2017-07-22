using System;
using MicrosoftHouse.Abstractions;
using System.Diagnostics;

namespace MicrosoftHouse
{
	public class SettingsViewModel : BaseViewModel
	{
		public SettingsViewModel()
		{
		}

        Boolean notificationsEnabled = true;
        public Boolean NotificationsEnabled
        {
            get { return notificationsEnabled; }
            set
            {
                SetProperty(ref notificationsEnabled, value, "NotificationsEnabled");
                Debug.WriteLine(notificationsEnabled);
            }
        }
	}
}
