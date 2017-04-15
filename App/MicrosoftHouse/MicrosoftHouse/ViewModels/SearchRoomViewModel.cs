using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Helpers;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class SearchRoomViewModel : BaseViewModel
	{
		ICloudService cloudService;

		public SearchRoomViewModel()
		{
			// Cloud Variables
			cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
			Table = cloudService.GetTable<Room>();

			SearchCommand = new Command(async () => await ExecuteSearchCommand());

			RefreshList();
		}

		public ICloudTable<Room> Table { get; set; }

		async Task RefreshList()
		{
			await ExecuteRefreshCommand();
			/*MessagingCenter.Subscribe<SelectedRoomViewModel>(this, "ItemsChanged", async (sender) =>
			{
				await ExecuteRefreshCommand();   
			});*/
		}

		Command refreshCmd;
		public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

		async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{

				var list = await Table.ReadAllRoomsAsync();

				// Rooms available now
				Rooms.Clear();
				foreach (var room in list)
				{
					Rooms.Add(room);
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

		public Command SearchCommand { get; }


		async Task ExecuteSearchCommand()
		{
			
		}
	}
}
