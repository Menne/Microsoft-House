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
            NewEventCommand = new Command(async () => await ExecuteNewEventCommand());
            SelectedDateCommand = new Command(async () => await ExecuteSelectedDateCommand());
            RetrieveEvents();
        }


        public Command NewEventCommand { get; }
        public Command SelectedDateCommand { get; }


        private DateTime? selectedDate;
        public DateTime? SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
           //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("selectedDate"));
            }
        }

        ObservableCollection<Event> eventsOfSelectedDay;
        public ObservableCollection<Event> EventsOfSelectedDay
        {
            get { return eventsOfSelectedDay; }
            set { SetProperty(ref eventsOfSelectedDay, value, "EventsOfSelectedDay"); }
        }

        List<Event> allEvents;
        public List<Event> AllEvents
        {
            get { return allEvents; }
            set { SetProperty(ref allEvents, value, "AllEvents"); }
        }

        private void RetrieveEvents()
        {
            AllEvents = new List<Event>
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

        async Task ExecuteNewEventCommand()
        {
             await Application.Current.MainPage.Navigation.PushModalAsync(new NewEventPage());
        }

        async Task ExecuteSelectedDateCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            EventsOfSelectedDay.Clear();
            foreach (var item in AllEvents)
                if (item.StartingDate.Date == SelectedDate)
                    EventsOfSelectedDay.Add(item);
            IsBusy = false;
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

    */



    }

}
