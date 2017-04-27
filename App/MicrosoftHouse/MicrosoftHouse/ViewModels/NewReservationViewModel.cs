using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Helpers;
using MicrosoftHouse.Models;
using Xamarin.Forms;
using System.Linq;

namespace MicrosoftHouse
{
    public class NewReservationViewModel : BaseViewModel
    {
        ICloudService cloudService;

        public NewReservationViewModel()
        {
            SearchAvailableRoomsCommand = new Command(async () => await ExecuteSearchAvailableRoomsCommand());
            NewReservationCommand = new Command(async () => await ExecuteNewReservationCommand());

            NewReservation = new Reservation();
            NewReservation.Date = DateTime.Now;
            NewReservation.StartingTime = DateTime.Now.TimeOfDay;
            NewReservation.EndingTime = DateTime.Now.TimeOfDay;

            RefreshList();
        }

        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();

        public Command SearchAvailableRoomsCommand { get; }
        public Command NewReservationCommand { get; }

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
                //await CloudService.SyncOfflineCacheAsync();
                var table = await CloudService.GetTableAsync<Room>();
                var list = await table.ReadAllRoomsAsync();

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

        ObservableCollection<Reservation> allReservations = new ObservableCollection<Reservation>();
        public ObservableCollection<Reservation> AllReservations
        {
            get { return allReservations; }
            set { SetProperty(ref allReservations, value, "AllReservations"); }
        }

        Reservation newReservation;
        public Reservation NewReservation
        {
            get { return newReservation; }
            set { SetProperty(ref newReservation, value, "NewReservation"); }
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
                    ExecuteNewReservationCommand();
                }
            }
        }

        async Task ExecuteSearchAvailableRoomsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var reservationTable = await CloudService.GetTableAsync<Reservation>();
                var listOfReservation = await reservationTable.ReadAllReservationsAsync();
                var roomTable = await CloudService.GetTableAsync<Room>();
                var listOfRooms = await roomTable.ReadAllRoomsAsync();

                Rooms.Clear();

                //insert all the rooms in the collection, then removes the ones which are arleady reserved
                foreach (Room room in listOfRooms)
                {
                    rooms.Add(room);
                    foreach (Reservation reservation in listOfReservation)
                    {
                        if (reservation.RoomName.Equals(room.Name) && reservation.Date.Date.Equals(NewReservation.Date.Date)
                            && TimeSpan.Compare(reservation.StartingTime, NewReservation.StartingTime)==1
                            && TimeSpan.Compare(reservation.EndingTime, NewReservation.EndingTime)==-1)
                        {
                            Rooms.Remove(room);
                        }
                    }
                }
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

        async Task ExecuteNewReservationCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var reservationTable = await CloudService.GetTableAsync<Reservation>();
                if (NewReservation.Id == null)
                {

                    Debug.WriteLine("Ciao");
                    // Get the identity
                    var identity = await CloudService.GetIdentityAsync();
                    if (identity != null)
                    {
                        var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value;
                        Debug.WriteLine(name);
                        NewReservation.User = name;
                        newReservation.RoomName = selectedRoom.Name;
                    }

                    await reservationTable.CreateReservationAsynch(NewReservation);
                    //await CloudService.SyncOfflineCacheAsync();
                }
                else
                {
                    await reservationTable.UpdateReservationAsync(NewReservation);
                    //await CloudService.SyncOfflineCacheAsync();
                }
                MessagingCenter.Send<NewReservationViewModel>(this, "ItemsChanged");
                SelectedRoom = null;
                await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PopAsync();
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