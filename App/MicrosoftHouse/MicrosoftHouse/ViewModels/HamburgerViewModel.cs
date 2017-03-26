using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class HamburgerViewModel : BaseViewModel
	{
		List<MasterPageItem> masterPageItems;

		public HamburgerViewModel()
		{
			masterPageItems = new List<MasterPageItem>();

			masterPageItems.Add(new MasterPageItem
			{
				Name = "Home",
				IconSource = "HomeIconWhite.png",
				TargetType = typeof(HomePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Rooms",
				IconSource = "RoomIconWhite.png",
				//TargetType = typeof(TodoListPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Calendar",
				IconSource = "CalendarIconWhite.png",
				//TargetType = typeof(ReminderPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Park",
				IconSource = "ParkIconWhite.png",
				//TargetType = typeof(ReminderPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "User",
				IconSource = "UserIconWhite.png",
				//TargetType = typeof(ReminderPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Settings",
				IconSource = "SettingsIconWhite.png",
				//TargetType = typeof(ReminderPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Logout",
				IconSource = "LogoutIconWhite.png",
				//TargetType = typeof(ReminderPage)
			});

		}

		public List<MasterPageItem> MasterPageItems 		{ 			set { SetProperty(ref masterPageItems, value, "Items"); } 			get { return masterPageItems; } 		}


	}
}
