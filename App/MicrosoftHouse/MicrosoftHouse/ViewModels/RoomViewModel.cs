using System;
using Xamarin.Forms;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using MicrosoftHouse.Helpers;

namespace MicrosoftHouse
{
	public class RoomViewModel : BaseViewModel
	{
		ICloudService cloudService;

		public RoomViewModel()
		{
			// Cloud Variables


			RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            SearchRoomCommand = new Command(async () => await ExecuteSearchCommand());
            RoomAVCommand = new Command(() => ExecuteRoomAVCommand());
			RoomALLCommand = new Command(() => ExecuteRoomALLCommand());
			RoomRECommand = new Command(() => ExecuteRoomRECommand());

			RefreshList();

			//Rooms = availableRooms;
			ExecuteRoomAVCommand();

		}

		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
		public Command RefreshCommand { get; }
		public Command SearchRoomCommand { get; }
		public Command RoomAVCommand { get; }
		public Command RoomALLCommand { get; }
		public Command RoomRECommand { get; }

		async Task RefreshList()
		{
			await ExecuteRefreshCommand();
			/*MessagingCenter.Subscribe<SelectedRoomViewModel>(this, "ItemsChanged", async (sender) =>
			{
				await ExecuteRefreshCommand();   
			});*/
		}

		async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				var table = await CloudService.GetTableAsync<Room>();
				var list = await table.ReadAllRoomsAsync();
				AllRooms.Clear();
				foreach (var room in list)
				{
					AllRooms.Add(room);
				}
					
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[RoomList] Error loading items: {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}



		ObservableCollection<Room> rooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> Rooms
		{
			get { return rooms; }
			set { SetProperty(ref rooms, value, "Rooms"); }
		}



		ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
		public ObservableCollection<Reservation> Reservations
		{
			get { return reservations; }
			set { SetProperty(ref reservations, value, "Reservations"); }
		}




		ObservableCollection<Room> availableRooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> AvailableRooms
		{
			get { return availableRooms; }
			set { SetProperty(ref availableRooms, value, "AvailableRooms"); }
		}

		ObservableCollection<Room> allRooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> AllRooms
		{
			get { return allRooms; }
			set { SetProperty(ref allRooms, value, "AllRooms"); }
		}

		ObservableCollection<Room> reservedRooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> ReservedRooms
		{
			get { return reservedRooms; }
			set { SetProperty(ref reservedRooms, value, "ReservedRooms"); }
		}


		Room selectedRoom;
		public Room SelectedRoom
		{
			get { return selectedRoom; }
			set
			{
				SetProperty(ref selectedRoom, value, "SelectedRoom");
				if (selectedRoom != null)
				{
					
					(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SelectedRoomPage(selectedRoom));
					SelectedRoom = null;	


				}
			}
		}

        

        async Task ExecuteSearchCommand()
        {
			await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SearchRoomPage());
        }

        public void ExecuteRoomAVCommand()
		{
			Rooms = availableRooms;
		}

		public void ExecuteRoomALLCommand()
		{
			Rooms = AllRooms;
		}

		public void ExecuteRoomRECommand()
		{
			Rooms = reservedRooms;
		}




	}
}
