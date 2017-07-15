using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Helpers;
using MicrosoftHouse.Models;
using MicrosoftHouse.ViewModels;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class NewEventViewModel : BaseViewModel
	{

		//ICloudTable<Event> table = App.CloudService.GetTable<Event>();
		//ICloudTable<EventLocation> table_location = App.CloudService.GetTable<EventLocation>();

		public NewEventViewModel()
		{
            //ICloudService cloudService = App.CloudService;

            CreateEventCommand = new Command(async () => await ExecuteCreateEventCommand());
            RefreshLocationsPickerCommand = new Command(async () => await ExecuteRefreshLocationsPickerCommand());

            Title = "New Event";
			           
			SelectedEvent = new Event();
			SelectedEvent.Date = DateTime.Now;
			SelectedEvent.StartingTime = DateTime.Now.TimeOfDay;
			SelectedEvent.EndingTime = DateTime.Now.TimeOfDay;
            //Event.Location = "LOCATION";

            RefreshLocationsPickerCommand.Execute(null);
		}

		public NewEventViewModel(Event selectedEvent = null)
		{
            // In this case ( Edit Command )
            CreateEventCommand = new Command(async () => await ExecuteCreateEventCommand());
            RefreshLocationsPickerCommand = new Command(async () => await ExecuteRefreshLocationsPickerCommand());

            SelectedEvent = selectedEvent;

            RefreshLocationsPickerCommand.Execute(null);
        }

		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
		public Command CreateEventCommand { get; }
        public Command RefreshLocationsPickerCommand { get; }


        ObservableCollection<EventLocation> locations = new ObservableCollection<EventLocation>();
        public ObservableCollection<EventLocation> Locations
        {
            get { return locations; }
            set { SetProperty(ref locations, value, "Locations"); }
        }

        ObservableCollection<string> locationsName = new ObservableCollection<string>();
        public ObservableCollection<string> LocationsName
        {
            get { return locationsName; }
            set { SetProperty(ref locationsName, value, "LocationsName"); }
        }

        Event selectedEvent;
        public Event SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                SetProperty(ref selectedEvent, value, "SelectedEvent");
            }
        }

        async Task ExecuteCreateEventCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				var tableEvent = await CloudService.GetTableAsync<Event>();
				if (SelectedEvent.Id == null)
				{
					// Get the identity
					var identity = await CloudService.GetIdentityAsync();
					if (identity != null)
					{
						var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value;
						SelectedEvent.User = name;
					}

					await tableEvent.CreateEventAsynch(SelectedEvent);
					//await CloudService.SyncOfflineCacheAsync();
				}
				else
				{
					await tableEvent.UpdateEventAsync(SelectedEvent);
					//await CloudService.SyncOfflineCacheAsync();
				}
				await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PopAsync();
                MessagingCenter.Send<NewEventViewModel>(this, "ItemsChanged");
            }
			catch (Exception ex)
			{
				Debug.WriteLine($"[TaskDetail] Save error: {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task ExecuteRefreshLocationsPickerCommand()
		{
			try
			{
				//await CloudService.SyncOfflineCacheAsync();

                var tableLocation = await CloudService.GetTableAsync<EventLocation>();
				var list = await tableLocation.ReadAllEventLocationsAsync();
				Locations.Clear();
				foreach (var location in list)
				{
					Locations.Add(location);
					LocationsName.Add(location.Name);
					Debug.WriteLine(location.Name);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[EventLocations] Error loading items: {ex.Message}");
			}
		}

		
    }
}
