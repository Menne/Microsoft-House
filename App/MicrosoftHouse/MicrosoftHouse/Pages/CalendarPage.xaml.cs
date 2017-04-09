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
            BindingContext = new CalendarViewModel();
        }

    }
}
