using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace MicrosoftHouse
{
	public class HomeViewModel : BaseViewModel
	{
		ZXingScannerPage scanPage;
		public HomeViewModel()
		{
			
			SearchRoomCommand = new Command(async () => await ExecuteSearchRoomCommand());
			RoomCommand = new Command(async () => await ExecuteRoomCommand());
			CalendarCommand = new Command(async () => await ExecuteCalendarCommand());
			ParkDetailCommand = new Command(async () => await ExecuteParkDetailCommand());
			NewEventCommand = new Command(async () => ExecuteNewEventCommand());
			NewParkCommand = new Command(async () => ExecuteNewParkCommand());

		}

		public Command SearchRoomCommand { get; }
		public Command RoomCommand { get; }
		public Command CalendarCommand { get; }
		public Command ParkDetailCommand { get; }
		public Command NewEventCommand { get; }
		public Command NewParkCommand { get; }

		async Task ExecuteCalendarCommand()
		{
			//await Application.Current.MainPage.Navigation.PushAsync(new RoomDetailPage());

			(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new CalendarPage())
			{
				//BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};		
		}

		async Task ExecuteParkDetailCommand()
		{
			//await Application.Current.MainPage.Navigation.PushAsync(new RoomDetailPage());

			(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new CarParkPage())
			{
				//BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};		
		}

		async Task ExecuteSearchRoomCommand()
		{
            /*Application.Current.MainPage = new NavigationPage(new SearchRoomPage())
			{
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};*/

            //await Application.Current.MainPage.Navigation.PushAsync(new SearchRoomPage());
            System.Diagnostics.Debug.WriteLine("ciao");

			//await Application.Current.MainPage.Navigation.PushModalAsync(new SearchRoomPage());

			// PERFETTTTTOOOO
			await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SearchRoomPage());



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
			//await Application.Current.MainPage.Navigation.PushModalAsync(new NewEventPage());

			await(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage());

            /*(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new NewEventPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};*/
        }

		// QRCODE

		async void ExecuteNewParkCommand()
		{
			var customOverlay = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(20, 30, 20, 30),
				Orientation = StackOrientation.Horizontal
			};
			var torch = new Button
			{
				Text = "Torch",
				TextColor = Color.White,
				FontFamily = "Avenir",
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.EndAndExpand,
				BackgroundColor = Color.FromHex("#FF01A4EF"),
				HeightRequest = 40,
				WidthRequest = 80,
				BorderRadius = 20
			};

			var close = new Button
			{
				Text = "X",
				TextColor = Color.White,
				FontFamily = "Avenir",
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				BackgroundColor = Color.FromHex("#FF01A4EF"),
				HeightRequest = 40,
				WidthRequest = 40,
				BorderRadius = 20
			};


			torch.Clicked += delegate
			{
				scanPage.ToggleTorch();
			};

			close.Clicked += delegate
			{
				Application.Current.MainPage.Navigation.PopModalAsync();
			};

			customOverlay.Children.Add(close);
			customOverlay.Children.Add(torch);




			scanPage = new ZXingScannerPage(customOverlay: customOverlay)
			{
				Title = "Park",
				Padding = new Thickness(0, Device.OnPlatform(20,0,0), 0, 0)
			};

			scanPage.OnScanResult += (result) =>
			{
				scanPage.IsScanning = false;

				Device.BeginInvokeOnMainThread(async () =>
				{
					await Application.Current.MainPage.Navigation.PopModalAsync();
					//Task<bool> task = DisplayAlert("Simple Alert", "Decide on an option", "Ok", "Cancel");
					//bool result = await task

					//await App.Current.MainPage.DisplayAlert("Park Done", result.Text, "OK");

					if (String.Equals(result.Text,"ParkingCode"))
					{
						
						await App.Current.MainPage.DisplayAlert("Park Done", "Thank You", "OK");
					}
				});
			};

			//await(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(scanPage);

			await Application.Current.MainPage.Navigation.PushModalAsync(scanPage);
		}
	}
}
