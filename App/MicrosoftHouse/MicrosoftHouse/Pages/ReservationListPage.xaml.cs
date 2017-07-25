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

            reservationListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                if (e.SelectedItem == null) return;
                ((ListView)sender).SelectedItem = null;
            };
        }
        

    }
}
