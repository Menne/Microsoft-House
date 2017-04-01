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
        int parkingSpaces=25, distance=3, timeToArrival=15;

        public ObservableCollection<Label> Statistics { get; set; }
        public Label SelectedDayStatistics { get; set; }

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

            ShowMondayStatistics = new Command(async () => await ExecuteShowMondayStatistics());
            ShowTuestdayStatistics = new Command(async () => await ExecuteShowTuestdayStatistics());
            ShowWednesdayStatistics = new Command(async () => await ExecuteWednesdayStatistics());
            ShowThursdayStatistics = new Command(async () => await ExecuteThursdayStatistics());
            ShowFridayStatistics = new Command(async () => await ExecuteFridayStatistics());
            ShowSaturdayStatistics = new Command(async () => await ExecuteSaturdayStatistics());
            ShowSundayStatistics = new Command(async () => await ExecuteSundayStatistics());

            SelectedDayStatistics = Statistics.ElementAt(0);
        }

        public Command ShowMondayStatistics { get; private set; }
        public Command ShowTuestdayStatistics { get; private set; }
        public Command ShowWednesdayStatistics { get; private set; }
        public Command ShowThursdayStatistics { get; private set; }
        public Command ShowFridayStatistics { get; private set; }
        public Command ShowSaturdayStatistics { get; private set; }
        public Command ShowSundayStatistics { get; private set; }

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

        
        private Task ExecuteSundayStatistics()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteSaturdayStatistics()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteFridayStatistics()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteThursdayStatistics()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteWednesdayStatistics()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteShowTuestdayStatistics()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteShowMondayStatistics()
        {
            throw new NotImplementedException();
        }


    }
}
