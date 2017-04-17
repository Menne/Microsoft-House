using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Helpers;
using MicrosoftHouse.Services;
using Xamarin.Forms;

namespace MicrosoftHouse
{
    public partial class App : Application
    {
		public static ICloudService CloudService { get; set; }

		public App()
        {
			ServiceLocator.Add<ICloudService, AzureCloudService>();
			//CloudService = new AzureCloudService();
			MainPage = new EntryPage();
			//MainPage = new RoomList();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
