using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            ChangeDayCommand = new Command<string>(execute: (string dayOfWeek) => ShowStatistics(Int32.Parse(dayOfWeek)));

            SelectedDayStatistics = Statistics.ElementAt(0);
        }


        int parkingSpaces = 25, distance = 3, timeToArrival = 15;

        public ObservableCollection<Label> Statistics { get; set; }
        public Label SelectedDayStatistics { get; set; }

        public Command ChangeDayCommand { get; private set; }

        public int ParkingSpaces
        {
            set { SetProperty(ref parkingSpaces, value); }
            get { return parkingSpaces; }
        }

        public int Distance
        {
            set { SetProperty(ref distance, value); }
            get { return distance; }
        }

        public int TimeToArrival
        {
            set { SetProperty(ref timeToArrival, value); }
            get { return timeToArrival; }
        }


        private void ShowStatistics(int dayOfWeek)
        {
            SelectedDayStatistics = Statistics.ElementAt(dayOfWeek);
        }
        

    }
}
