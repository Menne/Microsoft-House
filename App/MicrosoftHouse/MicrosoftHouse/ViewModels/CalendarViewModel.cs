using MicrosoftHouse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MicrosoftHouse.ViewModels
{
    class CalendarViewModel : ViewModelBase
    {
        ObservableCollection<Event> eventsProva = new ObservableCollection<Event>();
        Event selectedEvent;

        public ObservableCollection<Event> EventsProva
        {
            get { return eventsProva; }
            set { SetProperty(ref eventsProva, value, "Events"); }
        }
        
        public Event SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                SetProperty(ref selectedEvent, value, "SelectedEvent");
                if (selectedEvent != null)
                {
                    Application.Current.MainPage.Navigation.PushAsync(new EventDetailsPage());
                    selectedEvent = null;
                }
            }
        }
    }

}
