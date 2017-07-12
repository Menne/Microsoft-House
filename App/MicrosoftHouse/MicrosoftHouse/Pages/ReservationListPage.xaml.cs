using MicrosoftHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MicrosoftHouse.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationListPage : ContentPage
    {
        public ReservationListPage()
        {
            InitializeComponent();
            BindingContext = new ReservationListViewModel();
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender); //potrebbe servire
            DisplayAlert("Are you sure?", "Your reservation for this room will be deleted", "OK");
        }

    }
}
