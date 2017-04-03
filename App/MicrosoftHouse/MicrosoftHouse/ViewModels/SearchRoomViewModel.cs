using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class SearchRoomViewModel : BaseViewModel
	{
		public SearchRoomViewModel()
		{
			BackCommand = new Command(async () => await ExecuteBackCommand());
			SearchCommand = new Command(async () => await ExecuteSearchCommand());


			Room room = new Room();
			room.Name = "I01";
			room.Seats = "20";
			room.Floor = "1";

			Room room1 = new Room();
			room1.Name = "I01";
			room1.Seats = "20";
			room1.Floor = "1";

			Rooms.Add(room);
			Rooms.Add(room1);


		}

		ObservableCollection<Room> rooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> Rooms
		{
			get { return rooms; }
			set { SetProperty(ref rooms, value, "Rooms"); }
		}

		Room selectedRoom;
		public Room SelectedRoom
		{
			get { return selectedRoom; }
			set
			{
				SetProperty(ref selectedRoom, value, "SelectedItem");
				if (selectedRoom != null)
				{
					/*(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SelectedRoomPage(selectedRoom))
					{
						//BarTextColor = Color.White,
						BarBackgroundColor = Color.FromHex("#FF01A4EF")
					};*/

					Application.Current.MainPage.Navigation.PushModalAsync(new SelectedRoomPage(selectedRoom));
					SelectedRoom = null;
				}
			}
		}

		public Command BackCommand { get; }
		public Command SearchCommand { get; }


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

		async Task ExecuteSearchCommand()
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
