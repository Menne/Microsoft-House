using MicrosoftHouse.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamForms.Controls;

namespace MicrosoftHouse
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel();

            Calendar calendar= new Calendar
            {
                MinDate = DateTime.Now.AddDays(-1),
                MultiSelectDates = true,
                DisableAllDates = false,
                WeekdaysShow = true,
                ShowNumberOfWeek = true,
                ShowNumOfMonths = 1,
                EnableTitleMonthYearView = true,
                SelectedDate = DateTime.Now,
                WeekdaysTextColor = Color.Teal,
                StartDay = DayOfWeek.Monday,
                SelectedTextColor = Color.Fuchsia,
            };

        }

        async void OnCreateEvent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewEventPage());
        }
    }
}
