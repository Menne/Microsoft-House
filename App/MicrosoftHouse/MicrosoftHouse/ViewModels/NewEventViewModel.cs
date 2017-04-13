﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using MicrosoftHouse.ViewModels;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class NewEventViewModel : BaseViewModel
	{
		ICloudTable<Event> events_table = App.CloudService.GetTable<Event>();
		//ICloudTable<EventLocation> locations_table = App.CloudService.GetTable<EventLocation>();

		public NewEventViewModel()
		{
			Title = "New Event";

			Event = new Event();
			Event.Date = DateTime.Now;
			Event.StartingTime = DateTime.Now.TimeOfDay;
			Event.EndingTime = DateTime.Now.TimeOfDay;
			//Event.Location = "LOCATION";

			LoadEventLocations();

		}

		public NewEventViewModel(Event selectedEvent = null)
		{
			Event = selectedEvent;

		}

		Command cmdCreate;
		public Command CreateCommand => cmdCreate ?? (cmdCreate = new Command(async () => await ExecuteCreateCommand()));

		async Task ExecuteCreateCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				
				if (Event.Id == null)
				{
					await events_table.CreateEventAsynch(Event);
				}
				else
				{
					await events_table.UpdateEventAsync(Event);
				}
				//MessagingCenter.Send<NewEventViewModel>(this, "ItemsChanged");
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



		public Event Event { get; set; }

		async Task LoadEventLocations()
		{
			try
			{
				var table = App.CloudService.GetTable<EventLocation>();
				var list = await table.ReadAllEventLocationsAsync();
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

		public CalendarViewModel CalendarModel { get; set; }

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
