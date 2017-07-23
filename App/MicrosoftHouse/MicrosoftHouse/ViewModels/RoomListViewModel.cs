using System;
using Xamarin.Forms;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using MicrosoftHouse.Helpers;
using MicrosoftHouse.Pages;
using System.Linq;

namespace MicrosoftHouse
{
    public class RoomListViewModel : BaseViewModel
    {

        public RoomListViewModel()
        {
            ChangeAvailabilityLabelText();
            
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            NewReservationCommand = new Command(async () => await ExecuteNewReservationCommand());

            RefreshCommand.Execute(null);
        }


        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        public Command RefreshCommand { get; }
        public Command NewReservationCommand { get; }


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
                DisplayedRooms.Clear();
                foreach (var room in list)
                {
                    AllRooms.Add(room);
                    DisplayedRooms.Add(room);
                    SortRooms(DisplayedRooms, room);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[RoomListViewModel] Error loading items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Sorting algotirhm for the collection of rooms
        private void SortRooms(ObservableCollection<Room> source, Room item)
        {
            var oldIndex = source.IndexOf(item);
            var list = source.OrderBy(_ => _.Floor).ToList();
            var newIndex = list.IndexOf(item);

            source.Move(oldIndex, newIndex);
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

        String availabilityText = "prova";
        public String AvailabilityText
        {
            get { return availabilityText; }
            set { SetProperty(ref availabilityText, value, "AvailabilityText"); }
        }


        bool isAvailable = true;
        public bool IsAvailable
        {
            get { return isAvailable; }
            set
            {
                SetProperty(ref isAvailable, value, "IsAvailable");
                ChangeAvailabilityLabelText();
            }
        }

        String searchArgument = "";
        public String SearchArgument
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
            if (IsBusy)
                return;
            IsBusy = true;

            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewReservationPage());

            IsBusy = false;
        }

        void Search(String searchArgument)
        {
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
                    if (room.Name.Contains(searchArgument) || room.Floor.Contains(searchArgument))
                    {
                        DisplayedRooms.Add(room);
                    }
                }
            }
        }


        void ChangeAvailabilityLabelText()
        {
            if (IsAvailable)
            {
                AvailabilityText = "Available Now";
            }
            else
            {
                AvailabilityText = "Reserved by/n prova";
            }
                
        }
    }
}