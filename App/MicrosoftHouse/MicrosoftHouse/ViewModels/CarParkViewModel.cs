using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;
using MicrosoftHouse.Models;
using MicrosoftHouse.Helpers;
using Plugin.Geolocator;
using ZXing.Net.Mobile.Forms;

namespace MicrosoftHouse
{
    class CarParkViewModel : BaseViewModel
    {
        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        ZXingScannerPage scanPage;

        public CarParkViewModel()
        {
            InitializeStatistics();

            ChangeDayCommand = new Command<string>(execute: (string dayOfWeek) => ShowStatistics(Int32.Parse(dayOfWeek)));
            ParkCommand = new Command(async () => await ExecuteParkCommand());
            RefreshParkInfo = new Command(async () => await ExecuteRefreshParkInfo());
            RefreshUserPosition = new Command(async () => await ExecuteRefreshUserPosition());

            // Subscribe to a new parking from the scan page
            MessagingCenter.Subscribe<CarParkViewModel>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshParkInfo();
            });

            RefreshParkInfo.Execute(null);
            RefreshUserPosition.Execute(null);

            ShowStatistics(0);
        }

        public Command ChangeDayCommand { get; set; }
        public Command ParkCommand { get; set; }
        public Command RefreshParkInfo { get; set; }
        public Command RefreshUserPosition { get; set; }

        List<String> daysOfWeek = new List<String> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public List<String> DaysOfWeek
        { 
            get { return daysOfWeek; }
            set { SetProperty(ref daysOfWeek, value, "DaysOfWeek"); }
        }

        String selectedDayOfWeek;
        public String SelectedDayOfWeek
        {
            get { return selectedDayOfWeek; }
            set { SetProperty(ref selectedDayOfWeek, value, "SelectedDayOfWeek"); }
        }


        List<List<int>> statistics = new List<List<int>>();
        public List<List<int>> Statistics
        {
            get { return statistics; }
            set { SetProperty(ref statistics, value, "Statistics"); }
        }

        List<int> selectedDayStatistics = new List<int>();
        public List<int> SelectedDayStatistics
        {
            get { return selectedDayStatistics; }
            set { SetProperty(ref selectedDayStatistics, value, "SelectedDayStatistics"); }
        }


        int parkingSpaces;
        public int ParkingSpaces
        {
            set { SetProperty(ref parkingSpaces, value, "ParkingSpaces"); }
            get { return parkingSpaces; }
        }

        Double distance;
        public Double Distance
        {
            set { SetProperty(ref distance, value, "Distance"); }
            get { return distance; }
        }

        Double timeToArrival;
        public Double TimeToArrival
        {
            set { SetProperty(ref timeToArrival, value, "TimeToArrival"); }
            get { return timeToArrival; }
        }

        String parkButtonText;
        public String ParkButtonText
        {
            set { SetProperty(ref parkButtonText, value, "ParkButtonText"); }
            get { return parkButtonText; }
        }


        async Task ExecuteRefreshParkInfo()
        {
            try
            {
                var carParkTable = await CloudService.GetTableAsync<CarPark>();
                var park = await carParkTable.ReadAllParksAsync();
                ParkingSpaces = 100-park.Count();

                ParkButtonText = "PARK NOW";
                var identity = await CloudService.GetIdentityAsync();
                foreach (var slot in park)
                {
                    if (slot.Park == identity.UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value)
                    {
                        ParkButtonText = "LEAVE THE PARK";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CarParkViewModel] Error loading items: {ex.Message}");
            }
        }

        async Task ExecuteRefreshUserPosition()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync();
                if (position == null)
                    return;

                Debug.WriteLine("Position Latitude: {0}", position.Latitude);
                Debug.WriteLine("Position Longitude: {0}", position.Longitude);

                Distance = calculateDistance(position.Latitude, position.Longitude, 45.481739, 9.183140, 'K');
                TimeToArrival = (Math.Round((Distance / 30)*60, 1));
                // Per aggiustare il tempo
                TimeToArrival = TimeToArrival * 2;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }
        }

        private double calculateDistance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            // Per aggiustare la distanza
            dist = dist * 2;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (Math.Round(dist, 1));
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        private void InitializeStatistics()
        {
            Statistics = new List<List<int>>()
            {
                new List<int> {50,62,70,64,92,100,90,86,92,83,70,30},
                new List<int> {52,72,84,75,100,100,93,79,92,83,56,24},
                new List<int> {68,79,100,96,93,100,93,72,94,87,53,22},
                new List<int> {52,83,92,100,100,97,100,73,92,83,56,24},
                new List<int> {55,72,84,75,100,82,94,83,93,81,53,23},
                new List<int> {44,70,84,75,100,100,93,90,91,87,42,20},
                new List<int> {31,60,73,80,100,90,82,80,74,84,35,10},
            };
        }


        private void ShowStatistics(int dayOfWeek)
        {
            SelectedDayOfWeek = DaysOfWeek.ElementAt(dayOfWeek);
            SelectedDayStatistics = Statistics.ElementAt(dayOfWeek);
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
                    if (String.Equals(result.Text, "ParkingCode"))
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
                                MessagingCenter.Send<CarParkViewModel>(this, "ItemsChanged");
                                await App.Current.MainPage.DisplayAlert("Bye, " + name, "QR scanning went successfully", "OK");
                                await CloudService.SyncOfflineCacheAsync();
                            }
                            else
                            {
                                await carParkTable.CreateParkAsync(currentSlot);
                                await Application.Current.MainPage.Navigation.PopModalAsync();
                                MessagingCenter.Send<CarParkViewModel>(this, "ItemsChanged");
                                await App.Current.MainPage.DisplayAlert("Welcome, " + name, "QR scanning went successfully", "OK");
                                await CloudService.SyncOfflineCacheAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"[ScanPage] Error saving items: {ex.Message}");
                        }
                    }
                });
            };

            await Application.Current.MainPage.Navigation.PushModalAsync(scanPage);

            IsBusy = false;
        }
    }

}
