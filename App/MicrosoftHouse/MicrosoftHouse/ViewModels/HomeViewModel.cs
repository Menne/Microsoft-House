using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.ViewModels;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using MicrosoftHouse.Pages;
using MicrosoftHouse.Helpers;
using System.Linq;
using System.Diagnostics;
using MicrosoftHouse.Models;

namespace MicrosoftHouse
{
	/*TODO
	 * 1. aggiungere metodo park now in park view model, scalando il parcheggio ogni volta che uno utilizza il qrcode
	 * 2. aggiungere icone a tabbed page
	 * 3. implementare metodo search nella search room page
	 * 4. login page: togliere username e password, aggiungere icona microsoft al bottone
	 * 5. rimettere icone a pallini in Room e Park o qualcosa del genere
	 * 6. implementare metodo reserve per creare una reservation
	 * 7. capire perchè alla prima pagina non funziona mai il retrieve dei dati
	 * 8. all'apertura del calendario il giorno corrente deve essere selezionato
	 */

	public class HomeViewModel : BaseViewModel
	{
		ZXingScannerPage scanPage;

		public HomeViewModel()
		{

			LoadParkingInfo();

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

		string parkingSpaces;
		public string ParkingSpaces
		{
			set { SetProperty(ref parkingSpaces, value, "ParkingSpaces"); }
			get { return parkingSpaces; }
		}

		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();

		async void LoadParkingInfo()
		{
			try
			{
				//await CloudService.SyncOfflineCacheAsync();
				var carParkTable = await CloudService.GetTableAsync<CarPark>();
				var park = await carParkTable.ReadAllParksAsync();
				ParkingSpaces = park.ElementAt(0).Park;
				Debug.WriteLine(ParkingSpaces);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[ParkingSpaces] Error loading items: {ex.Message}");
			}
		}

		async Task ExecuteCalendarCommand()
		{
			//await Application.Current.MainPage.Navigation.PushAsync(new RoomDetailPage());

			(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new EventsPage())
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
			await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SearchRoomPage());
		}

		async Task ExecuteRoomCommand()
		{
			(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new RoomsPage())
			{
				//BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};
		}

		async Task ExecuteNewEventCommand()
		{
			await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage());
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

            scanPage = new ZXingScannerPage(customOverlay: customOverlay)
            {
                Title = "Park",
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0)
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
