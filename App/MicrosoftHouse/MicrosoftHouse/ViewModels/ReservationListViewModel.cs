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

            var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
            cloudService.GetIdentityAsync() ;

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
                await CloudService.SyncOfflineCacheAsync();
                var identity = await CloudService.GetIdentityAsync();
                if (identity != null)
                {
                    var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("urn:microsoftaccount:name")).Value;
                    var reservationTable = await CloudService.GetTableAsync<Reservation>();
                    var reservationList = await reservationTable.ReadAllReservationsAsync();
                    Reservations.Clear();
                    foreach (var reservation in reservationList)
                    {
                        if (reservation.User == name)
                        {
                            Reservations.Add(reservation);
                            SortReservations(Reservations, reservation);
                        }
                    }
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

        // Sorting algotirhm for the collection of reservations
        private void SortReservations(ObservableCollection<Reservation> source, Reservation item)
        {
            var oldIndex = source.IndexOf(item);
            var list = source.OrderBy(_ => _.Date).ThenBy(_ => _.StartingTime).ToList();
            var newIndex = list.IndexOf(item);

            source.Move(oldIndex, newIndex);
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
                    await table.DeleteReservationAsync(reservation);
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