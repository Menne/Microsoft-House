using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;
using MicrosoftHouse.Pages;
using MicrosoftHouse.Helpers;
using System.Diagnostics;
using System.Linq;

namespace MicrosoftHouse
{
	public class HamburgerViewModel : BaseViewModel
	{
		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();

        public HamburgerViewModel()
		{
			Title = "Hamburger menu";

			// Identity of the User
			LoadIdentity();

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
				Name = "Car Park",
                IconSource = Device.OnPlatform("ParkIconWhite.png", "ParkIconWhite.png", "Images/ParkIconWhite.png"),
                TargetType = typeof(CarParkPage)
			});
            masterPageItems.Add(new MasterPageItem
            {
                Name = "Events",
                IconSource = Device.OnPlatform("CalendarIconWhite.png", "CalendarIconWhite.png", "Images/CalendarIconWhite.png"),
                TargetType = typeof(EventsPage)
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
                IconSource = Device.OnPlatform("AboutUsIconWhite.png", "AboutUsIconWhite.png", "Images/AboutUsIconWhite.png"),
                TargetType = typeof(AboutUsPage)
            });
            masterPageItems.Add(new MasterPageItem
			{
				Name = "Logout",
                IconSource = Device.OnPlatform("LogoutIconWhite.png", "LogoutIconWhite.png", "Images/LogoutIconWhite.png"),
                TargetType = typeof(EntryPage)
			});

		}

        List<MasterPageItem> masterPageItems;
        public List<MasterPageItem> MasterPageItems
        {
            set { SetProperty(ref masterPageItems, value, "Items"); }
            get { return masterPageItems; }
        }


        string email;
        public string Email
        {
            set { SetProperty(ref email, value, "Email"); }
            get { return email; }
        }

        string user;
        public string User
        {
            set { SetProperty(ref user, value, "User"); }
            get { return user; }
        }


        async void LoadIdentity()
		{
			var identity = await CloudService.GetIdentityAsync();
			if (identity != null)
			{
				var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value;
				User = name;

				var mail = identity.UserId; //UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value;
				Email = mail;
			}
		}
	}
}
