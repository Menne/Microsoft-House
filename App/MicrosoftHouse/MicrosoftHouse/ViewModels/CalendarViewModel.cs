using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MicrosoftHouse.ViewModels
{
    class CalendarViewModel : BaseViewModel
    {
        public CalendarViewModel()
        {
            NewEventCommand = new Command(async () => await ExecuteNewEventCommand());
            RetrieveEvents();

			//DateTime time = DateTime.Now;

			//time.to
        }



		/*ObservableCollection<Room> rooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> Rooms
		{
			get { return rooms; }
			set { SetProperty(ref rooms, value, "Rooms"); }
		}*/

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

        ObservableCollection<Event> allEvents;
        public ObservableCollection<Event> AllEvents
        {
            get { return allEvents; }
            set { SetProperty(ref allEvents, value, "AllEvents"); }
        }

        private void RetrieveEvents()
        {
            AllEvents = new ObservableCollection<Event>
            {
                new Event
                {
                    Name="Evento 1",
                    Description = "Descrizione",
                    Place = "Aula 1",
                    StartingDate=DateTime.Now,
                    EndingDate=DateTime.Now,
                },
                new Event
                {
                    Name="Evento 2",
                    Description = "Descrizione",
                    Place = "Aula 2",
                    StartingDate=DateTime.Now,
                    EndingDate=DateTime.Now,
                },
                new Event
                {
                    Name="Evento 3",
                    Description = "Descrizione",
                    Place = "Aula 3",
                    StartingDate=DateTime.Now,
                    EndingDate=DateTime.Now,
                }
            };
        }

        async Task ExecuteNewEventCommand()
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new NewEventPage());
            await(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage());
        }

        private void ShowEventsOfTheDay()
        {
            EventsOfSelectedDay.Clear();
            foreach (Event item in AllEvents)
                if (item.StartingDate.Date == Date.Value.Date)
                    EventsOfSelectedDay.Add(item);
        }


    }

}
