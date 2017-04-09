using System;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class SelectedEventPageViewModel : BaseViewModel
	{
		public SelectedEventPageViewModel(Event selectedEvent = null)
		{
			if (selectedEvent != null)
			{
				Event = selectedEvent;
				//Title = selectedEvent.Name;
			}

			DeleteCommand = new Command(async () => await ExecuteDeleteCommand());
			EditCommand = new Command(async () => await ExecuteEditCommand());
		}

		public Event Event { get; set; }

		public Command DeleteCommand { get; }
		public Command EditCommand { get; }


		async Task ExecuteDeleteCommand()
		{
			//From the Bottom - Modal Page --> Aggiungere la Toolbar (Guardare il Capitolo)
			await Application.Current.MainPage.Navigation.PopModalAsync();

			/*(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new NewEventPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};*/
		}

		async Task ExecuteEditCommand()
		{
			(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewEventPage(Event));
		}
	}
}
