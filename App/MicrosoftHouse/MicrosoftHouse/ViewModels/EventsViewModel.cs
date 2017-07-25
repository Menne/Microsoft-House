using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using MicrosoftHouse.Helpers;
using XamForms.Controls;
using System.Collections.Generic;
using System.Linq;

namespace MicrosoftHouse.ViewModels
{
    public class EventsViewModel : BaseViewModel
    {
		ICloudService cloudService;

        public EventsViewModel()
        {
			// Commands
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            NewEventCommand = new Command(async () => await ExecuteNewEventCommand());
            DeleteEventCommand = new Command(async e => await ExecuteDeleteEventCommand((Event) e));

            // Subscribe to events from the new event page
            MessagingCenter.Subscribe<NewEventViewModel>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });
            // Subscribe to events from the selected event page
            MessagingCenter.Subscribe<SelectedEventViewModel>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });
            // Subscribe to push to sync notifications
            MessagingCenter.Subscribe<PushToSync>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });

            SelectedDate = DateTime.Now;

            // Execute the refresh command
            RefreshCommand.Execute(null);
        }

		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
		public Command RefreshCommand { get; }
		public Command NewEventCommand { get; }
        public Command DeleteEventCommand { get; }

        DateTime? selectedDate;
        public DateTime? SelectedDate
        {
            get { return selectedDate; }
            set
            {
                SetProperty(ref selectedDate, value, "SelectedDate");
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

        ObservableCollection<Event> eventsOfSelectedDate = new ObservableCollection<Event>();
        public ObservableCollection<Event> EventsOfSelectedDate
        {
            get { return eventsOfSelectedDate; }
            set { SetProperty(ref eventsOfSelectedDate, value, "EventsOfSelectedDate"); }
        }

		ObservableCollection<Event> allEvents = new ObservableCollection<Event>();
        public ObservableCollection<Event> AllEvents
        {
            get { return allEvents; }
            set { SetProperty(ref allEvents, value, "AllEvents"); }
        }

        ObservableCollection<SpecialDate> datesWithEvents = new ObservableCollection<SpecialDate>();
        public ObservableCollection<SpecialDate> DatesWithEvents
        {
            get { return datesWithEvents; }
            set { SetProperty(ref datesWithEvents, value, "DatesWithEvents"); }
        }

		async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
                var table = await CloudService.GetTableAsync<Event>();
				var list = await table.ReadAllEventsAsync();
				AllEvents.Clear();
                DatesWithEvents.Clear();
                foreach (var currentEvent in list)
				{
					AllEvents.Add(currentEvent);
                    SpecialDate specialDate = new SpecialDate(currentEvent.Date);
                    specialDate.Selectable = true;
                    specialDate.BorderColor = Color.White;
                    specialDate.BorderWidth = 2;
                    DatesWithEvents.Add(specialDate);
				}
                ShowEventsOfTheDay();
                await CloudService.SyncOfflineCacheAsync();
            }
			catch (Exception ex)
			{
				Debug.WriteLine($"[EventsViewModel] Error loading events: {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}

        async Task ExecuteNewEventCommand()
		{
            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage());
        }

        private void ShowEventsOfTheDay()
        {
            EventsOfSelectedDate.Clear();
            if (SelectedDate != null)
            { 
                foreach (Event e in AllEvents)
                {
                    if (e.Date.Date == SelectedDate.Value.Date)
                    {
                        EventsOfSelectedDate.Add(e);
                        SortEvents(EventsOfSelectedDate, e);
                    }
                }
            }
        }

        // Sorting algotirhm for the collection of events
        private void SortEvents(ObservableCollection<Event> source, Event item)
        {
            var oldIndex = source.IndexOf(item);
            var list = source.OrderBy(_ => _.StartingTime).ToList();
            var newIndex = list.IndexOf(item);

            source.Move(oldIndex, newIndex);
        }


        async Task ExecuteDeleteEventCommand(Event e)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Are you sure?", "Nobody will be able to see this event anymore from the shared calendar", "Yes", "No");
            if (!answer)
                return;

            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (e.Id != null)
                {
                    var table = await CloudService.GetTableAsync<Event>();
                    await table.DeleteEventAsync(e);
                    await CloudService.SyncOfflineCacheAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[EventsViewModel] Delete error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
            await ExecuteRefreshCommand();
        }

    }

}
