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

namespace MicrosoftHouse
{
    class CarParkViewModel : BaseViewModel
    {

        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();

        public CarParkViewModel()
        {
            InitializeParkInfo();
            InitializeUserPositionAsync();
            InitializeStatistics();

            ChangeDayCommand = new Command<string>(execute: (string dayOfWeek) => ShowStatistics(Int32.Parse(dayOfWeek)));

            ShowStatistics(0);
        }

        public Command ChangeDayCommand { get; private set; }

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

        async private void InitializeParkInfo()
        {
            try
            {
				//await CloudService.SyncOfflineCacheAsync();
                var carParkTable = await CloudService.GetTableAsync<CarPark>();
                var park = await carParkTable.ReadAllParksAsync();
                ParkingSpaces = Int32.Parse(park.ElementAt(0).Park);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ParkingSpaces] Error loading items: {ex.Message}");
            }
        }

        private async Task InitializeUserPositionAsync()
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
                new List<int> {6,4,3,45,1,3,7,8,9,10,11,3},
                new List<int> {4,2,5,2,5,6,7,8,9,10,11,34},
                new List<int> {5,3,3,4,5,6,7,8,2,10,11,4},
                new List<int> {1,2,3,7,9,3,0,1,9,10,11,4},
                new List<int> {1,2,3,4,3,1,7,8,9,3,11,12},
                new List<int> {1,2,3,4,6,4,1,8,9,3,6,3},
                new List<int> {1,2,3,4,5,6,2,4,9,4,11,12},
            };
        }


        private void ShowStatistics(int dayOfWeek)
        {
            SelectedDayOfWeek = DaysOfWeek.ElementAt(dayOfWeek);
            SelectedDayStatistics = Statistics.ElementAt(dayOfWeek);
        }


    }
}
