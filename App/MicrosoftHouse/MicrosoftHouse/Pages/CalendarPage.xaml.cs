using MicrosoftHouse.ViewModels;
using System;
using Xamarin.Forms;

namespace MicrosoftHouse
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();

			calendarProva.SelectedDate = DateTime.Now;

            BindingContext = new CalendarViewModel();
        }

    }
}
