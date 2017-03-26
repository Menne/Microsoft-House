using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftHouse.ViewModels
{
    class CarParkViewModel : ViewModelBase
    {
        int parkingSpaces=25, distance=3, timeToArrival=15;

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
