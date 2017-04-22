using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;
using MicrosoftHouse.Pages;

namespace MicrosoftHouse
{
	public class HamburgerViewModel : BaseViewModel
	{
		

		List<MasterPageItem> masterPageItems;

		public HamburgerViewModel()
		{
			Title = "Hamburger";

			masterPageItems = new List<MasterPageItem>();

            masterPageItems.Add(new MasterPageItem
            {
                Name = "Home",
                IconSource = Device.OnPlatform("HomeIconWhite.png","HomeIconWhite.png", "Images/HomeIconWhite.png"),
                TargetType = typeof(HomePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Rooms",
                IconSource = Device.OnPlatform("RoomIconWhite.png", "RoomIconWhite.png", "Images/RoomIconWhite.png"),
				TargetType = typeof(RoomsPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Events",
                IconSource = Device.OnPlatform("CalendarIconWhite.png", "CalendarIconWhite.png", "Images/CalendarIconWhite.png"),
                TargetType = typeof(EventsPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Car Park",
                IconSource = Device.OnPlatform("ParkIconWhite.png", "ParkIconWhite.png", "Images/ParkIconWhite.png"),
                TargetType = typeof(CarParkPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Settings",
                IconSource = Device.OnPlatform("SettingsIconWhite.png", "SettingsIconWhite.png", "Images/SettingsIconWhite.png"),
                TargetType = typeof(SettingsPage)
			});
            masterPageItems.Add(new MasterPageItem
            {
                Name = "About Us",
                IconSource = Device.OnPlatform("UserIconWhite.png", "UserIconWhite.png", "Images/UserIconWhite.png"),
                TargetType = typeof(AboutUsPage)
            });
            masterPageItems.Add(new MasterPageItem
			{
				Name = "Logout",
                IconSource = Device.OnPlatform("LogoutIconWhite.png", "LogoutIconWhite.png", "Images/LogoutIconWhite.png"),
                TargetType = typeof(EntryPage)
			});

		}

		public List<MasterPageItem> MasterPageItems
		{
			set { SetProperty(ref masterPageItems, value, "Items"); }
			get { return masterPageItems; }
		}


	}
}
