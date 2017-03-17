using System;
using System.Collections.ObjectModel;

namespace MicrosoftHouse
{
	public class AllRoomsBody : ViewModelBase
	{
		ObservableCollection<Room> rooms = new ObservableCollection<Room>();

		public ObservableCollection<Room> Rooms
		{
			set { SetProperty(ref rooms, value); }
			get { return rooms; }
		}

		public void RemoveRoom(Room room)
		{
			Rooms.Remove(room);
		}

		public void MoveStudentToTop(Room room)
		{
			Rooms.Move(Rooms.IndexOf(room), 0);
		}
	}
}
