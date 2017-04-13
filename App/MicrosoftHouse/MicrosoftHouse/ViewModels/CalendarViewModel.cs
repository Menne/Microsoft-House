using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

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

        async Task RetrieveEvents()
        {
			await ExecuteRefreshCommand();
			/*MessagingCenter.Subscribe<NewEventViewModel>(this, "ItemsChanged", async (sender) =>
			{
				await ExecuteRefreshCommand();   
			});*/
        }

		Command refreshCmd;
		public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

		async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				var table = App.CloudService.GetTable<Event>();
				var list = await table.ReadAllEventsAsync();
				AllEvents.Clear();
				foreach (var currentEvent in list)
				{
					AllEvents.Add(currentEvent);
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[EventList] Error loading items: {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
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
