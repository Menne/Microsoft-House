﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Helpers;
using Xamarin.Forms;
using System.Linq;

namespace MicrosoftHouse.ViewModels
{
	public class EntryPageViewModel : BaseViewModel
	{
		public EntryPageViewModel()
		{
			AppService = Locations.AppServiceUrl;
			LoginCommand = new Command(async () => await ExecuteLoginCommand());
        }

        public string AppService { get; set; }
		public Command LoginCommand { get; }


        async Task ExecuteLoginCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
                var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
                await cloudService.LoginAsync();   

                await cloudService.RegisterForPushNotifications(); //

                // Creando una nuova navigation page, non c'è più la possibilità di tornare indietro.
                Application.Current.MainPage = new MasterPage();

			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[Login] Error = {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
