using System;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class SelectedRoomViewModel : BaseViewModel
	{
		//ICloudTable<Room> table = App.CloudService.GetTable<Room>();

		public SelectedRoomViewModel(Room room = null)
		{
			if (room != null)
			{
				Room = room;
				Title = room.Name;
			}

			BackCommand = new Command(async () => await ExecuteBackCommand());
		}

		public Room Room { get; set; }


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
