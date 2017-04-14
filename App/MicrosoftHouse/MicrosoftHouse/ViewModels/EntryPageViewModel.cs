using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Helpers;
using Xamarin.Forms;

namespace MicrosoftHouse.ViewModels
{
	public class EntryPageViewModel : BaseViewModel
	{
		public EntryPageViewModel()
		{
			AppService = Locations.AppServiceUrl;
		}

		public string AppService { get; set; }

		Command loginCmd;
		public Command LoginCommand => loginCmd ?? (loginCmd = new Command(async () => await ExecuteLoginCommand()));

		async Task ExecuteLoginCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
				//await cloudService.LoginAsync(User);
				await cloudService.LoginAsync();
				// Creando una nuova navigation page, non cè più la possibilità di tornare indietro.
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
