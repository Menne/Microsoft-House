using MicrosoftHouse.ViewModels;

using Xamarin.Forms;

namespace MicrosoftHouse
{
    public partial class Calendar : ContentPage
    {
        public Calendar()
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel();
        }

    }
}
