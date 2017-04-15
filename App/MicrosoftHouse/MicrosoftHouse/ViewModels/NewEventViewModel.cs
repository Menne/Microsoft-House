using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

		ICloudService cloudService;

		public NewEventViewModel()
		{

			cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
			//ICloudService cloudService = App.CloudService;
			TableEvent = cloudService.GetTable<Event>();
			TableLocation = cloudService.GetTable<EventLocation>();

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
			cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
			TableEvent = cloudService.GetTable<Event>();
			TableLocation = cloudService.GetTable<EventLocation>();

			// In this case ( Edit Command )
			CreateCommand = new Command(async () => await ExecuteCreateCommand());

			SelectedEvent = selectedEvent;
		}

		public ICloudTable<Event> TableEvent { get; set; }
		public ICloudTable<EventLocation> TableLocation { get; set; }
		public Command CreateCommand { get; }


		async Task ExecuteCreateCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				
				if (SelectedEvent.Id == null)
				{

						/*var identity = await cloudService.GetIdentityAsync();
		                if (identity != null)
		                {
		                    var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("name")).Value;
							Title = $"Tasks for {name}";
		                }*/

					await TableEvent.CreateEventAsynch(SelectedEvent);
				}
				else
				{
					await TableEvent.UpdateEventAsync(SelectedEvent);
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
				var list = await TableLocation.ReadAllEventLocationsAsync();
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
