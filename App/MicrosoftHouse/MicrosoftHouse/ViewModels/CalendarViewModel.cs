using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MicrosoftHouse.ViewModels
{
    class CalendarViewModel : BaseViewModel
    {
        public CalendarViewModel()
        {
        //    RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            NewEventCommand = new Command(async () => await ExecuteNewEventCommand());
            EventsProva = new ObservableCollection<Event>
            {
                new Event
                {
                    Name="evento prova 1",
                    Description = "descrizione prova 1",
                    Place = "posto di prova 1",
                    StartingDate=DateTime.Now,
                    EndingDate=DateTime.Now,
                },
                new Event
                {
                    Name="evento prova 2",
                    Description = "descrizione prova 2",
                    Place = "posto di prova 2",
                    StartingDate=DateTime.Now,
                    EndingDate=DateTime.Now,
                },
                new Event
                {
                    Name="evento prova 3",
                    Description = "descrizione prova 3",
                    Place = "posto di prova 3",
                    StartingDate=DateTime.Now,
                    EndingDate=DateTime.Now,
                }
            };
        }


        public Command RefreshCommand { get; }
        public Command NewEventCommand { get; }

       
        ObservableCollection<Event> eventsProva;
        public ObservableCollection<Event> EventsProva
        {
            get { return eventsProva; }
            set { SetProperty(ref eventsProva, value, "Events"); }
        }


        async Task ExecuteNewEventCommand()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NewEventPage());
        }


        /* servirà per vedere i dettagli di evento/data

        Event selectedEvent;
        public Event SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                SetProperty(ref selectedEvent, value, "SelectedEvent");
                if (selectedEvent != null)
                {
                    Application.Current.MainPage.Navigation.PushAsync(new EventDetailsPage());
                    selectedEvent = null;
                }
            }
        }

             servirà per refreshare la lista di eventi
             
             async Task ExecuteRefreshCommand()
             {
                 if (IsBusy)
                     return;
                 IsBusy = true;

                 try
                 {
                     var table = App.CloudService.GetTable<Room>();
                     var list = await table.ReadAllRoomsAsync();
                     EventsProva.Clear();
                     foreach (var item in list)
                         EventsProva.Add(selectedEvent);
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


             async Task RefreshList()
             {
                 await ExecuteRefreshCommand();
                 MessagingCenter.Subscribe<RoomDetailViewModel>(this, "ItemsChanged", async (sender) =>
                 {
                     await ExecuteRefreshCommand();
                 });
             }  */
        
    }

}
