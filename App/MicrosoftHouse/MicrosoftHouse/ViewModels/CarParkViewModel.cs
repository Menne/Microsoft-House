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


        ObservableCollection<ObservableCollection<BoxView>> statistics = new ObservableCollection<ObservableCollection<BoxView>>();
        public ObservableCollection<ObservableCollection<BoxView>> Statistics
        {
            get { return statistics; }
            set { SetProperty(ref statistics, value, "Statistics"); }
        }

        ObservableCollection<BoxView> selectedDayStatistics = new ObservableCollection<BoxView>();
        public ObservableCollection<BoxView> SelectedDayStatistics
        {
            get { return selectedDayStatistics; }
            set { SetProperty(ref selectedDayStatistics, value, "SelectedDayStatistics"); }
        }

        Grid statisticsGrid = new Grid();
        public Grid StatisticsGrid
        {
            get { return statisticsGrid; }
            set { SetProperty(ref statisticsGrid, value, "StatisticsGrid"); }
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
            for (int i = 0; i < 7; i++)
            {
                ObservableCollection<BoxView> barChart = new ObservableCollection<BoxView>();
                for (int j = 0; j < 12; j++)
                {
                    BoxView boxView = new BoxView
                    {
                        Color = Color.FromHex("FF01A4EF"),
                        HeightRequest = 5*j,
                        VerticalOptions = LayoutOptions.End,
                    };
                    Debug.WriteLine(boxView.Height);
                    barChart.Add(boxView);
                }
                Statistics.Add(barChart);
            }
        }


        private void ShowStatistics(int dayOfWeek)
        {
            SelectedDayOfWeek = DaysOfWeek.ElementAt(dayOfWeek);
            SelectedDayStatistics = Statistics.ElementAt(dayOfWeek);
            StatisticsGrid.Children.Clear();
            StatisticsGrid.Children.AddHorizontal(selectedDayStatistics);
   //         foreach (BoxView box in StatisticsGrid.Children)
   //             Debug.WriteLine(box.Height.ToString());
        }


    }
}
