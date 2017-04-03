using System;
using Xamarin.Forms;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System.Collections.ObjectModel;

namespace MicrosoftHouse
{
	public class RoomViewModel : BaseViewModel
	{
		public RoomViewModel()
		{
			RoomAVCommand = new Command(() => ExecuteRoomAVCommand());
			RoomALLCommand = new Command(() => ExecuteRoomALLCommand());
			RoomRECommand = new Command(() => ExecuteRoomRECommand());

			// Available
			Room room = new Room();
			room.Name = "I01";
			room.Seats = "20";
			room.Floor = "1";

			Room room1 = new Room();
			room1.Name = "I01";
			room1.Seats = "20";
			room1.Floor = "1";

			AvailableRooms.Add(room);
			AvailableRooms.Add(room1);

			// All
			Room room2 = new Room();
			room2.Name = "I02";
			room2.Seats = "20";
			room2.Floor = "1";

			Room room3 = new Room();
			room3.Name = "I02";
			room3.Seats = "20";
			room3.Floor = "1";

			AllRooms.Add(room2);
			AllRooms.Add(room3);

			// Available
			Room room4 = new Room();
			room4.Name = "I03";
			room4.Seats = "20";
			room4.Floor = "1";

			Room room5 = new Room();
			room5.Name = "I03";
			room5.Seats = "20";
			room5.Floor = "1";

			ReservedRooms.Add(room4);
			ReservedRooms.Add(room5);

			Rooms = availableRooms;


		}

		ObservableCollection<Room> rooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> Rooms
		{
			get { return rooms; }
			set { SetProperty(ref rooms, value, "Rooms"); }
		}


		ObservableCollection<Room> availableRooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> AvailableRooms
		{
			get { return availableRooms; }
			set { SetProperty(ref availableRooms, value, "Rooms"); }
		}

		ObservableCollection<Room> allRooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> AllRooms
		{
			get { return allRooms; }
			set { SetProperty(ref allRooms, value, "Rooms"); }
		}

		ObservableCollection<Room> reservedRooms = new ObservableCollection<Room>();
		public ObservableCollection<Room> ReservedRooms
		{
			get { return reservedRooms; }
			set { SetProperty(ref reservedRooms, value, "Rooms"); }
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
					Application.Current.MainPage.Navigation.PushModalAsync(new SelectedRoomPage(selectedRoom));
					SelectedRoom = null;
				}
			}
		}


		public Command RoomAVCommand { get; }
		public Command RoomALLCommand { get; }
		public Command RoomRECommand { get; }

		public void ExecuteRoomAVCommand()
		{
			Rooms = availableRooms;
		}

		public void ExecuteRoomALLCommand()
		{
			Rooms = allRooms;
		}

		public void ExecuteRoomRECommand()
		{
			Rooms = reservedRooms;
		}




	}
}
