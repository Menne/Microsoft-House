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
            var mi = ((MenuItem)sender); //potrebbe servire
            DisplayAlert("Are you sure?", "This event will be deleted, and will disappear from everybody's calendar!", "OK");
        }

    }
}
