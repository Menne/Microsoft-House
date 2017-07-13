using MicrosoftHouse.ViewModels;
using System;
using Xamarin.Forms;

namespace MicrosoftHouse
{
    public partial class EventsPage : ContentPage
    {
        public EventsPage()
        {
            InitializeComponent();
            BindingContext = new EventsViewModel();
        }

    }
}
