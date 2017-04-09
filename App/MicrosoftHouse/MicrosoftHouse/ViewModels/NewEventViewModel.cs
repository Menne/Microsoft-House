using System;
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
