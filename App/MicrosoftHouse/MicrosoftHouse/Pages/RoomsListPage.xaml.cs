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
    public partial class RoomsListPage : ContentPage
    {
        public RoomsListPage()
        {
            InitializeComponent();
            BindingContext = new RoomsListViewModel();
        }

        public ListView ListView { get { return listView; } }
    }
}
