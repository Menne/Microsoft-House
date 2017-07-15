using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Helpers;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class SelectedEventViewModel : BaseViewModel
	{
		//ICloudTable<Event> table = App.CloudService.GetTable<Event>();

		public SelectedEventViewModel(Event selectedEvent = null)
		{
            if (selectedEvent != null)
            {
                SelectedEvent = selectedEvent;
                //Title = selectedEvent.Name;
            }

            DeleteEventCommand = new Command(async () => await ExecuteDeleteEventCommand());
			EditEventCommand = new Command(async () => await ExecuteEditEventCommand());
		}

		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
		public Command DeleteEventCommand { get; }
		public Command EditEventCommand { get; }


        Event selectedEvent;
        public Event SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                SetProperty(ref selectedEvent, value, "SelectedEvent");
            }
        }

		async Task ExecuteDeleteEventCommand()
		{
			if (IsBusy)
                return;
            IsBusy = true;

			try
			{
				if (SelectedEvent.Id != null)
				{
					var table = await CloudService.GetTableAsync<Event>();
					await table.DeleteEventAsync(SelectedEvent);
				}
				await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PopAsync();
                MessagingCenter.Send<SelectedEventViewModel>(this, "ItemsChanged");
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

		async Task ExecuteEditEventCommand()
		{
            if (IsBusy)
                return;
            IsBusy = true;

            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage(SelectedEvent));

            IsBusy = false;
        }
	}
}
