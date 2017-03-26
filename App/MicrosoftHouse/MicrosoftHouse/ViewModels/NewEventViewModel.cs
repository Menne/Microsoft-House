using System;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class NewEventViewModel : BaseViewModel
	{

        public NewEventViewModel()
		{
			Title = "New Event";
            
			BackCommand = new Command(async () => await ExecuteBackCommand());

		}

		public Command BackCommand { get; }


		async Task ExecuteBackCommand()
		{
			//From the Bottom - Modal Page --> Aggiungere la Toolbar (Guardare il Capitolo)
			await Application.Current.MainPage.Navigation.PopModalAsync();

			/*(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new NewEventPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};*/
		}
    }
}
