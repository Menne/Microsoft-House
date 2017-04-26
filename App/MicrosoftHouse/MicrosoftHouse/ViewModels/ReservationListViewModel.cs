using System;
using Xamarin.Forms;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using MicrosoftHouse.Helpers;
using MicrosoftHouse.Pages;

namespace MicrosoftHouse
{
    public class ReservationListViewModel : BaseViewModel
    {
        ICloudService cloudService;

        public ReservationListViewModel()
        {
            // Cloud Variables


            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            NewRoomReservationCommand = new Command(async () => await ExecuteNewRoomReservationCommand());

            RefreshList();

        }

        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        public Command RefreshCommand { get; }
        public Command NewRoomReservationCommand { get; }

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
				//await CloudService.SyncOfflineCacheAsync();
                var table = await CloudService.GetTableAsync<Room>();
                var list = await table.ReadAllRoomsAsync();
                ReservedRooms.Clear();
                foreach (var room in list)
                {
                    ReservedRooms.Add(room);
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



        async Task ExecuteNewRoomReservationCommand()
        {
            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewReservationPage());
        }


    }
}