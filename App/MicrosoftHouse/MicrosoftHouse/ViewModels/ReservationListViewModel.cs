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
        public ReservationListViewModel()
        {
            // Cloud Variables


            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            NewReservationCommand = new Command(async () => await ExecuteNewReservationCommand());
            DeleteReservationCommand = new Command(async reservation => await ExecuteDeleteReservationCommand((Reservation) reservation));

            RefreshList();

        }

        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        public Command RefreshCommand { get; }
        public Command NewReservationCommand { get; }
        public Command DeleteReservationCommand { get; }

        async Task RefreshList()
        {
            await ExecuteRefreshCommand();
            MessagingCenter.Subscribe<SelectedRoomViewModel>(this, "ItemsChanged", async (sender) =>
			{
				await ExecuteRefreshCommand();   
			});
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
                Reservations.Clear();
                foreach (var reservation in reservationList)
                {
                    Reservations.Add(reservation);   
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


        ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
        public ObservableCollection<Reservation> Reservations
        {
            get { return reservations; }
            set { SetProperty(ref reservations, value, "Reservations"); }
        }

        async Task ExecuteNewReservationCommand()
        {
            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewReservationPage());
        }


        async Task ExecuteDeleteReservationCommand(Reservation reservation)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (reservation.Id != null)
                {
                    var table = await CloudService.GetTableAsync<Reservation>();
                    await table.DeleteEventAsync(reservation);
                }

                //MessagingCenter.Send<SelectedEventPageViewModel>(this, "ItemsChanged");
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
            await ExecuteRefreshCommand();
        }

    }
}