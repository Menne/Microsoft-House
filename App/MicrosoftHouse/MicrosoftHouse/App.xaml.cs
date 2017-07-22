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


        public App(string loadParameter = null)
        {
            ServiceLocator.Add<ICloudService, AzureCloudService>();

            if (loadParameter == null)
            {
                var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
                if (cloudService.GetIdentityAsync() == null)
                {
                    MainPage = new NavigationPage(new EntryPage());
                }
                else
                {
                    if (loadParameter == "eventsync")
                    {
                        MainPage = new NavigationPage(new EntryPage());
                    }
                    else
                    {
                        MainPage = new NavigationPage(new EntryPage());
                    }
                }
            }
            
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
