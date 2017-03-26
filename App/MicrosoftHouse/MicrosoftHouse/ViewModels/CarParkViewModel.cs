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

        public ObservableCollection<Label> ItemsForCarousel { get; set; }

        public CarParkViewModel()
        {
            ItemsForCarousel = new ObservableCollection<Label>
            {
                new Label
                {
                    Text="carousel1"
                },
                new Label
                {
                    Text="carousel2"
                },
                new Label
                {
                    Text="carousel3"
                }
            };
        }

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

    }
}
