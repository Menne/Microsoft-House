using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace MicrosoftHouse
{
    public partial class MainPage : ContentPage
    {

		AzureManager manager;
		
        public MainPage()
        {
            InitializeComponent();

			manager = new AzureManager();

			//CurrentPlatform.Init();


			/// How to add images from code.
			/*Image image = new Image
			{
				Source = new FileImageSource
				{
					File = Device.OnPlatform(iOS: "Icon-76.png",
											 Android: "icon.png"
											 WinPhone: "Assets/StoreLogo.png")
				},
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			*/

        }

		async void OnRoomList(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RoomList());
		}

		async void OnParking(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CarPark());
		}

		async void OnSignUp(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SignUp());
		}

		async void OnPiani(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Piani());
		}

		async void OnPianiCode(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PianiCode());
		}

		async void OnAvailableRooms(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewRoomList());
		}

		async void OnAvailableRoomsMVVC(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AllRoomsView());
		}

		async void OnCreateEvent(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewEvent());
		}

		public async void Insert(object sender, EventArgs e)
		{
			Room room = new Room
			{
				Name = "Prova",
				Piano = "Piano 1"
			};

			await AddRoom(room);
		}

		async Task AddRoom(Room room)
		{
			await manager.SaveTaskAsync(room);
		}

		/* ALLERT BOX */
		async void OnAlertBox(object sender, EventArgs e)
		{
			Task<bool> task = DisplayAlert("Simple Alert", "Decide on an option", "Ok", "Cancel");
			label.Text = "Alert is currently displayed";
			bool result = await task;
			label.Text = String.Format("Alert {0} button was pressed",
			result ? "OK" : "Cancel");
		}
	}
}
