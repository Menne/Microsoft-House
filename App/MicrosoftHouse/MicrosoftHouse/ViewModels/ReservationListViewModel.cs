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
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            NewReservationCommand = new Command(async () => await ExecuteNewReservationCommand());
            DeleteReservationCommand = new Command(async reservation => await ExecuteDeleteReservationCommand((Reservation) reservation));

            //Subscribe to event from the selected room page
            MessagingCenter.Subscribe<SelectedRoomViewModel>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });
            //Subscribe to event from the new reservation page
            MessagingCenter.Subscribe<NewReservationViewModel>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });

            // Execute the refresh command
            RefreshCommand.Execute(null);
        }

        public ICloudService CloudService => ServiceLocator.Get<ICloudService>();
        public Command RefreshCommand { get; }
        public Command NewReservationCommand { get; }
        public Command DeleteReservationCommand { get; }


        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
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
                Debug.WriteLine($"[ReservationListViewModel] Error loading items: {ex.Message}");
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
            var answer = await Application.Current.MainPage.DisplayAlert("Are you sure?", "Your reservation for this room will be deleted", "Yes", "No");
            if (!answer)
                return;

            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (reservation.Id != null)
                {
                    var table = await CloudService.GetTableAsync<Reservation>();
                    await table.DeleteEventAsync(reservation);
                    await CloudService.SyncOfflineCacheAsync();
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
            await ExecuteRefreshCommand();
        }

    }
}