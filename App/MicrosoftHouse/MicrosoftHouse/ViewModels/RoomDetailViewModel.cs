using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class RoomDetailViewModel : BaseViewModel
	{
		ICloudTable<Room> table = App.CloudService.GetTable<Room>();

		public RoomDetailViewModel(Room room = null)
		{
			if (room != null)
			{
				Room = room;
				Title = room.Name;
			}
			else
			{
				Room = new Room { Name = "New Room", Floor = 1, Seats = 30 };
				Title = "New Item";
			}
		}

		public Room Room { get; set; }

		Command cmdSave;
		public Command SaveCommand => cmdSave ?? (cmdSave = new Command(async () => await ExecuteSaveCommand()));

		async Task ExecuteSaveCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				if (Room.Id == null)
				{
					await table.CreateRoomAsynch(Room);
				}
				else
				{
					await table.UpdateRoomAsync(Room);
				}
				MessagingCenter.Send<RoomDetailViewModel>(this, "ItemsChanged");
				await Application.Current.MainPage.Navigation.PopAsync();
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

		Command cmdDelete;
		public Command DeleteCommand => cmdDelete ?? (cmdDelete = new Command(async () => await ExecuteDeleteCommand()));

		async Task ExecuteDeleteCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				if (Room.Id != null)
				{
					await table.DeleteRoomAsync(Room);
				}
				MessagingCenter.Send<RoomDetailViewModel>(this, "ItemsChanged");
				await Application.Current.MainPage.Navigation.PopAsync();
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
	}
}
