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

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Are you sure?", mi.CommandParameter + " will be deleted", "OK");
        }

    }
}
