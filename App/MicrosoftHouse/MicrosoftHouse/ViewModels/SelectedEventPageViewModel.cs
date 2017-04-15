﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Helpers;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class SelectedEventPageViewModel : BaseViewModel
	{
		//ICloudTable<Event> table = App.CloudService.GetTable<Event>();
		//ICloudService cloudService;

		public SelectedEventPageViewModel(Event selectedEvent = null)
		{
			//cloudService = App.CloudService;
			//TableEvent = cloudService.GetTable<Event>();

			RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
			DeleteCommand = new Command(async () => await ExecuteDeleteCommand());
			EditCommand = new Command(async () => await ExecuteEditCommand());

			if (selectedEvent != null)
			{
				SelectedEvent = selectedEvent;
				//Title = selectedEvent.Name;
			}

			RetrieveEvent();

		}

		//public ICloudTable<Event> TableEvent { get; set; }
		//public ICloudTable<EventLocation> TableLocation { get; set; }
		public Command RefreshCommand { get; }
		public Command DeleteCommand { get; }
		public Command EditCommand { get; }


		async Task RetrieveEvent()
		{
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
				var table = App.CloudService.GetTable<Event>();
				var element = await table.ReadEventAsync(SelectedEvent.Id);
				SelectedEvent = element;

				Debug.WriteLine(SelectedEvent.Name);
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
		Event selectedEvent;
		public Event SelectedEvent
		{
			get { return selectedEvent; }
			set
			{
				SetProperty(ref selectedEvent, value, "SelectedEvent");
			}
		}


		async Task ExecuteDeleteCommand()
		{
			if (IsBusy)
                return;
            IsBusy = true;

			try
			{
				if (SelectedEvent.Id != null)
				{
					var table = App.CloudService.GetTable<Event>();
					await table.DeleteEventAsync(SelectedEvent);
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
		}

		async Task ExecuteEditCommand()
		{
			(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage(SelectedEvent));
		}
	}
}
