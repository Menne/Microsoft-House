using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MicrosoftHouse.ViewModels
{
    class CarParkViewModel : ViewModelBase
    {

        public CarParkViewModel()
        {
            InitializeParkInfo();
            InitializeDays();
            InitializeStatistics();

            ChangeDayCommand = new Command<string>(execute: (string dayOfWeek) => ShowStatistics(Int32.Parse(dayOfWeek)));

            ShowStatistics(0);
        }


        public Command ChangeDayCommand { get; private set; }

        ObservableCollection<Label> daysOfWeek;
        public ObservableCollection<Label> DaysOfWeek
        { 
            get { return daysOfWeek; }
            set { SetProperty(ref daysOfWeek, value, "DaysOfWeek"); }
        }

        Label selectedDayOfWeek = new Label();
        public Label SelectedDayOfWeek
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
            set { SetProperty(ref parkingSpaces, value); }
            get { return parkingSpaces; }
        }

        int distance;
        public int Distance
        {
            set { SetProperty(ref distance, value); }
            get { return distance; }
        }

        int timeToArrival;
        public int TimeToArrival
        {
            set { SetProperty(ref timeToArrival, value); }
            get { return timeToArrival; }
        }

        private void InitializeParkInfo()
        {
            parkingSpaces = 25;
            distance = 3;
            timeToArrival = 15;
        }

        private void InitializeDays()
        {
            DaysOfWeek = new ObservableCollection<Label>
            {
                new Label()
                {
                    Text = "monday"
                },
                new Label()
                {
                    Text = "tuesday"
                },
                new Label()
                {
                    Text = "wednesday"
                },
                new Label()
                {
                    Text = "thursday"
                },
                new Label()
                {
                    Text = "friday"
                },
                new Label()
                {
                    Text = "saturday"
                },
                new Label()
                {
                    Text = "sunday"
                },
            };
        }

        private void InitializeStatistics()
        {
            Statistics = new List<List<int>>()
            {
                new List<int> {0,4,3,45,1,3,7,8,9,10,11,3},
                new List<int> {21,2,3,4,5,6,7,8,9,10,11,34},
                new List<int> {1,2,3,4,5,6,7,8,9,10,11,4},
                new List<int> {1,2,3,4,5,6,7,8,9,10,11,4},
                new List<int> {1,2,3,4,5,6,7,8,9,10,11,12},
                new List<int> {1,2,3,4,5,6,7,8,9,10,11,12},
                new List<int> {1,2,3,4,5,6,7,8,9,10,11,12},
            };
        }


        private void ShowStatistics(int dayOfWeek)
        {
            SelectedDayOfWeek = DaysOfWeek.ElementAt(dayOfWeek);
            SelectedDayStatistics = Statistics.ElementAt(dayOfWeek);
        }


    }
}
