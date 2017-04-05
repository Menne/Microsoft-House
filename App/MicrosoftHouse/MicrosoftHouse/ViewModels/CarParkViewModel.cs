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
            RetrieveParkInfo();
            RetrieveStatistics();
            InitializePlot();
            
            ChangeDayCommand = new Command<string>(execute: (string dayOfWeek) => ShowStatistics(Int32.Parse(dayOfWeek)));

            SelectedDayStatistics = Statistics.ElementAt(0);
        }


        public Command ChangeDayCommand { get; private set; }

        ObservableCollection<Label> statistics;
        public ObservableCollection<Label> Statistics
        { 
            get { return statistics; }
            set { SetProperty(ref statistics, value, "Statistics"); }
        }

        Label selectedDayStatistics = new Label();
        public Label SelectedDayStatistics
        {
            get { return selectedDayStatistics; }
            set { SetProperty(ref selectedDayStatistics, value, "SelectedDayStatistics"); }
        }


        PlotModel statisticsChart = new PlotModel();
        public PlotModel StatisticsChart
        {
            get { return statisticsChart; }
            set { SetProperty(ref statisticsChart, value, "StatisticsChart"); }
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


        private void RetrieveParkInfo()
        {
            parkingSpaces = 25;
            distance = 3;
            timeToArrival = 15;
        }

        private void RetrieveStatistics()
        {
            Statistics = new ObservableCollection<Label>
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

        private void ShowStatistics(int dayOfWeek)
        {
            SelectedDayStatistics = Statistics.ElementAt(dayOfWeek);
            Debug.WriteLine(Statistics.ElementAt(dayOfWeek).Text);
        }

        private void InitializePlot()
        {
            statisticsChart.Title = "prova";
            statisticsChart.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -20, Maximum = 80 });
            statisticsChart.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -20, Maximum = 80 });
        }


    }
}
