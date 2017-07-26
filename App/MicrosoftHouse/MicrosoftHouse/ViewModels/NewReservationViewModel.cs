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
        public NewReservationViewModel()
        {
            SearchAvailableRoomsCommand = new Command(async () => await ExecuteSearchAvailableRoomsCommand());
            CreateReservationCommand = new Command(async () => await ExecuteCreateReservationCommand());

            NewReservation = new Reservation();
            NewReservation.Date = DateTime.Now;
            NewReservation.StartingTime = DateTime.Now.TimeOfDay;
            NewReservation.EndingTime = DateTime.Now.TimeOfDay;
        }

        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        public Command RefreshListCommand { get; }
        public Command SearchAvailableRoomsCommand { get; }
        public Command CreateReservationCommand { get; }


        ObservableCollection<Room> availableRooms = new ObservableCollection<Room>();
        public ObservableCollection<Room> AvailableRooms
        {
            get { return availableRooms; }
            set { SetProperty(ref availableRooms, value, "AvailableRooms"); }
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
                    NewReservation.RoomName = selectedRoom.Name;
                    CreateReservationCommand.Execute(null);
                }
            }
        }

        async Task ExecuteSearchAvailableRoomsCommand()
        {
            if (NewReservation.StartingTime > NewReservation.EndingTime)
            {
                await Application.Current.MainPage.DisplayAlert("Something is wrong", "A reservation cannot finish before it starts!", "OK");
                return;
            }

            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var reservationTable = await CloudService.GetTableAsync<Reservation>();
                var listOfReservations = await reservationTable.ReadAllReservationsAsync();
                var roomTable = await CloudService.GetTableAsync<Room>();
                var listOfRooms = await roomTable.ReadAllRoomsAsync();

                AvailableRooms.Clear();

                //inserts all the rooms in the collection, then removes the ones which are arleady reserved
                foreach (Room room in listOfRooms)
                {
                    AvailableRooms.Add(room);
                    SortRooms(AvailableRooms, room);
                    foreach (Reservation reservation in listOfReservations)
                    {
                        if (reservation.RoomName.Equals(room.Name))
                        {
                            if (reservation.Date.Date.Equals(NewReservation.Date.Date))
                            {
                                if ((TimeSpan.Compare(reservation.StartingTime, NewReservation.EndingTime) == -1
                                    && TimeSpan.Compare(reservation.EndingTime, NewReservation.EndingTime) == 1) ||
                                   (TimeSpan.Compare(reservation.StartingTime, NewReservation.StartingTime) == -1
                                    && TimeSpan.Compare(reservation.EndingTime, NewReservation.StartingTime) == 1) ||
                                   (TimeSpan.Compare(reservation.StartingTime, NewReservation.StartingTime) == 1
                                    && TimeSpan.Compare(reservation.EndingTime, NewReservation.EndingTime) == -1))
                                {
                                    AvailableRooms.Remove(room);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ReservationListViewModel] Save error: {ex.Message}");
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
            var list = source.OrderBy(_ => _.Floor).ThenBy(_ => _.Name).ToList();
            var newIndex = list.IndexOf(item);

            source.Move(oldIndex, newIndex);
        }

        async Task ExecuteCreateReservationCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var reservationTable = await CloudService.GetTableAsync<Reservation>();
                if (NewReservation.Id == null)
                {

                    // Get the identity
                    var identity = await CloudService.GetIdentityAsync();
                    if (identity != null)
                    {
                        var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value;
                        NewReservation.User = name;
                    }

                    await reservationTable.CreateReservationAsync(NewReservation);
                }
                else
                {
                    await reservationTable.UpdateReservationAsync(NewReservation);
                }
                SelectedRoom = null;
                await CloudService.SyncOfflineCacheAsync();
                await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PopAsync();
                MessagingCenter.Send<NewReservationViewModel>(this, "ItemsChanged");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[NewReservationViewModel] Save error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}