using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MicrosoftHouse.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        public CalendarViewModel()
        {
            NewEventCommand = new Command(async () => await ExecuteNewEventCommand());
            RetrieveEvents();
        }

		Event selectedEvent;
		public Event SelectedEvent
		{
			get { return selectedEvent; }
			set
			{
				SetProperty(ref selectedEvent, value, "SelectedEvent");
				if (selectedEvent != null)
				{
					/*(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SelectedRoomPage(selectedRoom))
					{
						//BarTextColor = Color.White,
						BarBackgroundColor = Color.FromHex("#FF01A4EF")
					};*/

					(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SelectedEventPage(selectedEvent));
					//Application.Current.MainPage.Navigation.PushModalAsync(new SelectedRoomPage(selectedRoom));
					selectedEvent = null;
				}
			}
		}

		public Command NewEventCommand { get; }

        DateTime? date;
        public DateTime? Date
        {
            get { return date; }
            set { SetProperty(ref date, value, "SelectedDate");
                if (date != null)
                    ShowEventsOfTheDay(); 
            }
        }

        ObservableCollection<Event> eventsOfSelectedDay = new ObservableCollection<Event>();
        public ObservableCollection<Event> EventsOfSelectedDay
        {
            get { return eventsOfSelectedDay; }
            set { SetProperty(ref eventsOfSelectedDay, value, "EventsOfSelectedDay"); }
        }

		ObservableCollection<Event> allEvents = new ObservableCollection<Event>();
        public ObservableCollection<Event> AllEvents
        {
            get { return allEvents; }
            set { SetProperty(ref allEvents, value, "AllEvents"); }
        }

        private void RetrieveEvents()
        {
			AllEvents.Add(new Event
			{
				Name = "Evento 1",
				Description = "Descrizione",
				Place = "Aula 1",
				Date = DateTime.Now,
			});
			AllEvents.Add(new Event
			{
				Name = "Evento 2",
				Description = "Descrizione",
				Place = "Aula 2",
				Date = DateTime.Now,
			});
			AllEvents.Add(new Event
			{
				Name = "Evento 3",
				Description = "Descrizione",
				Place = "Aula 3",
				Date = DateTime.Now,
			});
              
        }

		// Aggiungi un EVENTO
		/*public void addEvent(Event newEvent)
		{
			AllEvents.Add(newEvent);
		}*/

        async Task ExecuteNewEventCommand()
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new NewEventPage());
            await(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage());
        }

        private void ShowEventsOfTheDay()
        {
            EventsOfSelectedDay.Clear();
            foreach (Event item in AllEvents)
                if (item.Date.Date == Date.Value.Date)
                    EventsOfSelectedDay.Add(item);
        }


    }

}
