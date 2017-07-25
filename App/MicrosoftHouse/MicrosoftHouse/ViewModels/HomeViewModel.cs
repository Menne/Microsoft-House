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
	public class HomeViewModel : BaseViewModel
	{
		ZXingScannerPage scanPage;

		public HomeViewModel()
		{
            LoadInfoCommand = new Command(async () => await ExecuteLoadInfoCommand());
            SearchRoomCommand = new Command(async () => await ExecuteSearchRoomCommand());
			RoomCommand = new Command(async () => await ExecuteRoomCommand());
			CalendarCommand = new Command(async () => await ExecuteCalendarCommand());
			ParkDetailCommand = new Command(async () => await ExecuteParkDetailCommand());
			NewEventCommand = new Command(async () => ExecuteNewEventCommand());
			NewParkCommand = new Command(async () => ExecuteParkCommand());

            LoadInfoCommand.Execute(null);
        }

        public Command LoadInfoCommand { get; }
        public Command SearchRoomCommand { get; }
		public Command RoomCommand { get; }
		public Command CalendarCommand { get; }
		public Command ParkDetailCommand { get; }
		public Command NewEventCommand { get; }
		public Command NewParkCommand { get; }


		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();


        async Task ExecuteLoadInfoCommand()
        {
            try
            {
                await CloudService.SyncOfflineCacheAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[HomeViewModel] Sync error: {ex.Message}");
            }
        }


		async Task ExecuteCalendarCommand()
		{
            if (IsBusy)
                return;
            IsBusy = true;

            (Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new EventsPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#2196F3")
			};

            IsBusy = false;
		}

		async Task ExecuteParkDetailCommand()
		{
            if (IsBusy)
                return;
            IsBusy = true;

            (Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new CarParkPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#2196F3")
			};

            IsBusy = false;
        }

		async Task ExecuteSearchRoomCommand()
		{
            if (IsBusy)
                return;
            IsBusy = true;

            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewReservationPage());

            IsBusy = false;
        }

		async Task ExecuteRoomCommand()
		{
            if (IsBusy)
                return;
            IsBusy = true;

            (Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new RoomsPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#2196F3")
			};

            IsBusy = false;
        }

		async Task ExecuteNewEventCommand()
		{
            if (IsBusy)
                return;
            IsBusy = true;

            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage());

            IsBusy = false;
        }

        // QRCODE
        async Task ExecuteParkCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

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
                    try
                    {
                        var carParkTable = await CloudService.GetTableAsync<CarPark>();
                        var listOfSlots = await carParkTable.ReadAllRoomsAsync();
                        String name = "";

                        // Get the identity
                        var identity = await CloudService.GetIdentityAsync();
                        if (identity != null)
                        {
                            name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value;
                        }
                        CarPark currentSlot = new CarPark();
                        currentSlot.Park = name;
                        Debug.WriteLine(currentSlot.Park);
                        Debug.WriteLine(name);

                        bool hasParked = false;
                        foreach (var slot in listOfSlots)
                        {
                            if (slot.Park.Equals(currentSlot.Park))
                            {
                                hasParked = true;
                                currentSlot = slot;
                                break;
                            }
                        }
                        Debug.WriteLine(hasParked);

                        if (hasParked)
                        {
                            await carParkTable.DeleteParkAsync(currentSlot);
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                            await App.Current.MainPage.DisplayAlert("Bye, " + name, "QR scanning went successfully", "OK");
                            await CloudService.SyncOfflineCacheAsync();
                        }
                        else
                        {
                            await carParkTable.CreateParkAsync(currentSlot);
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                            await App.Current.MainPage.DisplayAlert("Welcome, " + name, "QR scanning went successfully", "OK");
                            await CloudService.SyncOfflineCacheAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[ScanPage] Error saving items: {ex.Message}");
                    }
                });
            };

            await Application.Current.MainPage.Navigation.PushModalAsync(scanPage);

            IsBusy = false;
        }
    }
}
