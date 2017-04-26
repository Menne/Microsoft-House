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

			CreateCommand = new Command(async () => await ExecuteCreateCommand());

			Title = "New Event";
			           
			SelectedEvent = new Event();
			SelectedEvent.Date = DateTime.Now;
			SelectedEvent.StartingTime = DateTime.Now.TimeOfDay;
			SelectedEvent.EndingTime = DateTime.Now.TimeOfDay;
			//Event.Location = "LOCATION";

			LoadEventLocations();


		}

		public NewEventViewModel(Event selectedEvent = null)
		{
			// In this case ( Edit Command )
			CreateCommand = new Command(async () => await ExecuteCreateCommand());

			SelectedEvent = selectedEvent;
		}

		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
		public Command CreateCommand { get; }


		async Task ExecuteCreateCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				var tableEvent = await CloudService.GetTableAsync<Event>();
				if (SelectedEvent.Id == null)
				{

					Debug.WriteLine("Ciao");
					// Get the identity
					var identity = await CloudService.GetIdentityAsync();
					if (identity != null)
					{
						var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value;
						Debug.WriteLine(name);
						SelectedEvent.User = name;
					}

					await tableEvent.CreateEventAsynch(SelectedEvent);
					await CloudService.SyncOfflineCacheAsync();
				}
				else
				{
					await tableEvent.UpdateEventAsync(SelectedEvent);
					await CloudService.SyncOfflineCacheAsync();
				}
				MessagingCenter.Send<NewEventViewModel>(this, "ItemsChanged");
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

		async Task LoadEventLocations()
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
    }
}
