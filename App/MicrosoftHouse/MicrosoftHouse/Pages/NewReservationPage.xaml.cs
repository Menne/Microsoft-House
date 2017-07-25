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
    public partial class NewReservationPage : ContentPage
    {
        public NewReservationPage()
        {
            InitializeComponent();
            BindingContext = new NewReservationViewModel();

            datePicker.MinimumDate = DateTime.Now;
        }

        public void OnTimePicker(object sender, EventArgs e)
        {
            timePickerFirst.Focus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (Application.Current.MainPage as MasterDetailPage).IsGestureEnabled = false;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (Application.Current.MainPage as MasterDetailPage).IsGestureEnabled = true;
        }

        public void OnDatePicker(object sender, EventArgs e)
        {
            datePicker.Focus();
        }

    }
}
