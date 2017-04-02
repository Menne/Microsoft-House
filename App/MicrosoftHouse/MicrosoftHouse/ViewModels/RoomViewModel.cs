using System;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class RoomViewModel
	{
		public RoomViewModel()
		{
			AvRoomCoomand = new Command(() =>  ExecuteAvRoomCommand());
			AllRoomCommand = new Command(() => ExecuteAllRoomsCommand());
			ReservedRoomCommand = new Command( () =>  ExecuteReservedRoomCommand());
		}

		public Command AvRoomCoomand { get; }
		public Command AllRoomCommand { get; }
		public Command ReservedRoomCommand { get; }

		public void ExecuteAvRoomCommand()
		{
			
		}

		public void ExecuteAllRoomsCommand()
		{

		}

		public void ExecuteReservedRoomCommand()
		{

		}


	}
}
