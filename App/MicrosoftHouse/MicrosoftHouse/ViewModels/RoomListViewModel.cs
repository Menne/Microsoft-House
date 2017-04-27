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
    public class RoomListViewModel : BaseViewModel
    {
        ICloudService cloudService;

        public RoomListViewModel()
        {
            // Cloud Variables


            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            NewReservationCommand = new Command(async () => await ExecuteNewReservationCommand());

            RefreshList();
        }


        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        public Command RefreshCommand { get; }
        public Command NewReservationCommand { get; }

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
                AllRooms.Clear();
                foreach (var room in list)
                {
                    allRooms.Add(room);
                    displayedRooms.Add(room);
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

        ObservableCollection<Room> displayedRooms = new ObservableCollection<Room>();
        public ObservableCollection<Room> DisplayedRooms
        {
            get { return displayedRooms; }
            set { SetProperty(ref displayedRooms, value, "DisplayedRooms"); }
        }

        string searchArgument = "";
        public string SearchArgument
        {
            get { return searchArgument; }
            set
            {
                SetProperty(ref searchArgument, value, "SearchArgument");
                Search(searchArgument);
            }
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
        

        async Task ExecuteNewReservationCommand()
        {
            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewReservationPage());
        }

        void Search(String searchArgument)
        {
            Debug.WriteLine(searchArgument);
            if (searchArgument.Equals(""))
            { 
                foreach (Room room in AllRooms)
                {
                    DisplayedRooms.Add(room);
                }
            }
            else
            { 
                DisplayedRooms.Clear();
                foreach (Room room in AllRooms)
                {
                    if (room.Name.Equals(searchArgument) || room.Floor.Equals(searchArgument))
                    {
                        DisplayedRooms.Add(room);
                    }
                }
            }
        }

    }
}