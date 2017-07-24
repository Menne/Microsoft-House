using System;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MicrosoftHouse.Helpers;
using System.Linq;

namespace MicrosoftHouse
{
	public class SelectedRoomViewModel : BaseViewModel
	{
		
		public SelectedRoomViewModel(Room room = null)
		{
			if (room != null)
			{
                SelectedRoom = room;
				Title = room.Name;
			}

            NewReservation = new Reservation();
            NewReservation.Date = DateTime.Now;
            NewReservation.StartingTime = DateTime.Now.TimeOfDay;
            NewReservation.EndingTime = DateTime.Now.TimeOfDay;

            NewReservationCommand = new Command(async () => await ExecuteNewReservationCommand());
            RefreshReservationsCommand = new Command(async () => await ExecuteRefreshReservationsCommand());

            RefreshReservationsCommand.Execute(null);
        }

        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        public Room SelectedRoom { get; set; }
        public Reservation NewReservation { get; set; }
		public Command NewReservationCommand { get; }
        public Command RefreshReservationsCommand { get; }

        ObservableCollection<Reservation> reservationsOfSelectedRoom = new ObservableCollection<Reservation>();
        public ObservableCollection<Reservation> ReservationsOfSelectedRoom
        {
            get { return reservationsOfSelectedRoom; }
            set { SetProperty(ref reservationsOfSelectedRoom, value, "ReservationsOfSelectedRoom"); }
        }

        async Task ExecuteRefreshReservationsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await CloudService.SyncOfflineCacheAsync();
                var reservationTable = await CloudService.GetTableAsync<Reservation>();
                var reservationList = await reservationTable.ReadAllReservationsAsync();
                ReservationsOfSelectedRoom.Clear();
                foreach (var reservation in reservationList)
                {
                    if (reservation.RoomName.Equals(SelectedRoom.Name))
                    {
                        ReservationsOfSelectedRoom.Add(reservation);
                        SortReservations(ReservationsOfSelectedRoom, reservation);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Reservations] Error loading items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Sorting algotirhm for the collection of reservations
        private void SortReservations(ObservableCollection<Reservation> source, Reservation item)
        {
            var oldIndex = source.IndexOf(item);
            var list = source.OrderBy(_ => _.Date).ThenBy(_ => _.StartingTime).ToList();
            var newIndex = list.IndexOf(item);

            source.Move(oldIndex, newIndex);
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

                    // Get the identity
                    var identity = await CloudService.GetIdentityAsync();
                    if (identity != null)
                    {
                        var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value;
                        Debug.WriteLine(name);
                        NewReservation.User = name;
                        NewReservation.RoomName = SelectedRoom.Name;
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
                MessagingCenter.Send<SelectedRoomViewModel>(this, "ItemsChanged");
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
