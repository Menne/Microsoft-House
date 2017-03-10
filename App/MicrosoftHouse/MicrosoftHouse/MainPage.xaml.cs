using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MicrosoftHouse
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

		async void OnRoomList(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RoomList());
		}

		async void OnCalendar(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Calendar());
		}

    }
}
