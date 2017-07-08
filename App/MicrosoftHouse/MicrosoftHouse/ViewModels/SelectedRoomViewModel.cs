using System;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MicrosoftHouse.Helpers;

namespace MicrosoftHouse
{
	public class SelectedRoomViewModel : BaseViewModel
	{
		
		public SelectedRoomViewModel(Room room = null)
		{
			if (room != null)
			{
				Room = room;
				Title = room.Name;
			}

			BackCommand = new Command(async () => await ExecuteBackCommand());

            RefreshList();
        }

        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        public Room Room { get; set; }
		public Command BackCommand { get; }

        ObservableCollection<Reservation> reservationsOfSelectedRoom = new ObservableCollection<Reservation>();
        public ObservableCollection<Reservation> ReservationsOfSelectedRoom
        {
            get { return reservationsOfSelectedRoom; }
            set { SetProperty(ref reservationsOfSelectedRoom, value, "ReservationsOfSelectedRoom"); }
        }

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
                var reservationTable = await CloudService.GetTableAsync<Reservation>();
                var reservationList = await reservationTable.ReadAllReservationsAsync();
                ReservationsOfSelectedRoom.Clear();
                foreach (var reservation in reservationList)
                {
                    if (reservation.RoomName.Equals(Room.Name))
                    {
                        ReservationsOfSelectedRoom.Add(reservation);
                        Debug.WriteLine(reservation.RoomName);
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


        async Task ExecuteBackCommand()
		{
			//From the Bottom - Modal Page --> Aggiungere la Toolbar (Guardare il Capitolo)
			await Application.Current.MainPage.Navigation.PopModalAsync();

			/*(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new NewEventPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};*/
		}

	}
}
