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
    public class RoomListViewModel : BaseViewModel
    {
        ICloudService cloudService;

        public RoomListViewModel()
        {
            // Cloud Variables


            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            SearchRoomCommand = new Command(async () => await ExecuteSearchCommand());

            RefreshList();
        }

        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        public Command RefreshCommand { get; }
        public Command SearchRoomCommand { get; }

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
				await CloudService.SyncOfflineCacheAsync();
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


        ObservableCollection<Room> allRooms = new ObservableCollection<Room>();
        public ObservableCollection<Room> AllRooms
        {
            get { return allRooms; }
            set { SetProperty(ref allRooms, value, "AllRooms"); }
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

    }
}