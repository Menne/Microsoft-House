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
			Title = "Hamburger";

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
				TargetType = typeof(RoomNavPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Calendar",
				IconSource = "CalendarIconWhite.png",
				TargetType = typeof(CalendarPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Park",
				IconSource = "ParkIconWhite.png",
				TargetType = typeof(CarParkPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Name = "Settings",
				IconSource = "SettingsIconWhite.png",
				TargetType = typeof(SettingsPage)
			});
            masterPageItems.Add(new MasterPageItem
            {
                Name = "About Us",
                IconSource = "UserIconWhite.png",
          		TargetType = typeof(AboutUsPage)
            });
            masterPageItems.Add(new MasterPageItem
			{
				Name = "Logout",
				IconSource = "LogoutIconWhite.png",
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
