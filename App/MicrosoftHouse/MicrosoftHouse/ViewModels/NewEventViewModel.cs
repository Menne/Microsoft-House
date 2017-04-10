using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using MicrosoftHouse.ViewModels;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class NewEventViewModel : BaseViewModel
	{

		public NewEventViewModel()
		{
			Title = "New Event";

			BackCommand = new Command(() => ExecuteBackCommand());
			CreateCommand = new Command(() => ExecuteCreateCommand());

			Event = new Event();

			// LOCATIONS PER GLI EVENTI

			// Available
			EventLocation location = new EventLocation();
			location.Name = "AULA MAGNA";
			location.Seats = "200";
			location.Floor = "1";

			EventLocation location1 = new EventLocation();
			location1.Name = "ATRIO";
			location1.Seats = "100";
			location1.Floor = "1";

			Locations.Add(location);
			Locations.Add(location1);

			foreach (EventLocation eventLocation in Locations)
				LocationsName.Add(eventLocation.Name);

		}

		public NewEventViewModel(Event selectedEvent = null)
		{
			Event = selectedEvent;

			BackCommand = new Command(() => ExecuteBackCommand());
			CreateCommand = new Command(() => ExecuteCreateCommand());
		}

		public Event Event { get; set; }
		public CalendarViewModel CalendarModel { get; set; }

		public Command BackCommand { get; }
		public Command CreateCommand { get; }

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


		public void ExecuteBackCommand()
		{
			Application.Current.MainPage.Navigation.PopModalAsync();
		}

		public void ExecuteCreateCommand()
		{
			System.Diagnostics.Debug.WriteLine(Event.Name);
			//CalendarModel.addEvent(Event);

			Application.Current.MainPage.Navigation.PopModalAsync();
		}
    }
}
