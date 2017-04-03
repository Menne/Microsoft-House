using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class HomeViewModel : BaseViewModel
	{
		public HomeViewModel()
		{
			
			SearchRoomCommand = new Command(async () => await ExecuteSearchRoomCommand());
			RoomCommand = new Command(async () => await ExecuteRoomCommand());
			NewEventCommand = new Command(async () => await ExecuteNewEventCommand());
		}

		public Command SearchRoomCommand { get; }
		public Command RoomCommand { get; }
		public Command NewEventCommand { get; }

		async Task ExecuteSearchRoomCommand()
		{
			/*Application.Current.MainPage = new NavigationPage(new SearchRoomPage())
			{
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};*/

			await Application.Current.MainPage.Navigation.PushModalAsync(new SearchRoomPage());

			/*(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SearchRoomPage())
			{
				//BarTextColor = Color.White,
				//BarTitleFontFamily = "Avenir",
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};*/
		}

		async Task ExecuteRoomCommand()
		{
			//await Application.Current.MainPage.Navigation.PushAsync(new RoomDetailPage());

			(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new RoomNavPage())
			{
				//BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};
		}

		async Task ExecuteNewEventCommand()
		{
			//From the Bottom - Modal Page --> Aggiungere la Toolbar (Guardare il Capitolo)
			await Application.Current.MainPage.Navigation.PushModalAsync(new NewEventPage());

			/*(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new NewEventPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};*/
		}
	}
}
