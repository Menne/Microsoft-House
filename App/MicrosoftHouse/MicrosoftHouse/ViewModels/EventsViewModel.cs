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

            // First Method to run
            RetrieveEvents();
        }

		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
		public Command RefreshCommand { get; }
		public Command NewEventCommand { get; }
        public Command DeleteEventCommand { get; }

        DateTime? date;
        public DateTime? Date
        {
            get { return date; }
            set
            {
                SetProperty(ref date, value, "SelectedDate");
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

        ObservableCollection<XamForms.Controls.SpecialDate> allEventDates = new ObservableCollection<XamForms.Controls.SpecialDate>();
        public ObservableCollection<XamForms.Controls.SpecialDate> AllEventDates
        {
            get { return allEventDates; }
            set { SetProperty(ref allEventDates, value, "AllEventDates"); }
        }

        async Task RetrieveEvents()
        {
			await ExecuteRefreshCommand();
			MessagingCenter.Subscribe<NewEventViewModel>(this, "ItemsChanged", async (sender) =>
			{
				await ExecuteRefreshCommand();   
			});
            MessagingCenter.Subscribe<SelectedEventViewModel>(this, "ItemsChanged", async (sender) =>
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
                AllEventDates.Clear();
				foreach (var currentEvent in list)
				{
					AllEvents.Add(currentEvent);
                    XamForms.Controls.SpecialDate specialDate=new XamForms.Controls.SpecialDate(currentEvent.Date);
                    specialDate.Selectable = true;
                    specialDate.BorderColor = Color.White;
                    specialDate.BorderWidth = 2;
                    AllEventDates.Add(specialDate);
				}

                //Date = DateTime.Now;

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
            if (IsBusy)
                return;
            IsBusy = true;

            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage());

            IsBusy = false;
        }

        private void ShowEventsOfTheDay()
        {
            EventsOfSelectedDay.Clear();
            if (Date!=null)
            { 
                foreach (Event item in AllEvents)
                {
                    if (item.Date.Date == Date.Value.Date)
                    {
                        EventsOfSelectedDay.Add(item);
                    }
                }
            }
        }


        async Task ExecuteDeleteEventCommand(Event e)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (e.Id != null)
                {
                    var table = await CloudService.GetTableAsync<Event>();
                    await table.DeleteEventAsync(e);
                }

                //MessagingCenter.Send<SelectedEventPageViewModel>(this, "ItemsChanged");
                await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskDetail] Save error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
            await ExecuteRefreshCommand();
        }

    }

}
