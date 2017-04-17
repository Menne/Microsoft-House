using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using MicrosoftHouse.Helpers;

namespace MicrosoftHouse.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
		ICloudService cloudService;

        public CalendarViewModel()
        {
			// Commands
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            NewEventCommand = new Command(async () => await ExecuteNewEventCommand());

			// First Method to run
			RefreshCommand.Execute(null);
        }

		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
		public Command RefreshCommand { get; }
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

		Event selectedEvent;
		public Event SelectedEvent
		{
			get { return selectedEvent; }
			set
			{
				SetProperty(ref selectedEvent, value, "SelectedEvent");
				if (selectedEvent != null)
				{
					(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SelectedEventPage(selectedEvent));
					selectedEvent = null;
				}
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
			MessagingCenter.Subscribe<NewEventViewModel>(this, "ItemsChanged", async (sender) =>
			{
				await ExecuteRefreshCommand();   
			});
        }

		async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				//await CloudService.SyncOfflineCacheAsync();
				var table = await CloudService.GetTableAsync<Event>();
				var list = await table.ReadAllEventsAsync();
				AllEvents.Clear();
				foreach (var currentEvent in list)
				{
					AllEvents.Add(currentEvent);
				}

				await CloudService.SyncOfflineCacheAsync();

				Date = DateTime.Now;

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

        async Task ExecuteNewEventCommand()
		{
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
