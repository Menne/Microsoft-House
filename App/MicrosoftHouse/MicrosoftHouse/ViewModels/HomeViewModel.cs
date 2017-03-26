using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class HomeViewModel : BaseViewModel
	{
		public HomeViewModel()
		{
			SearchRoomCommand = new Command(async () => await ExecuteSearchRoomCommand());
		}

		public Command SearchRoomCommand { get; }

		async Task ExecuteSearchRoomCommand()
		{
			//await Application.Current.MainPage.Navigation.PushAsync(new RoomDetailPage());
			(Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SearchRoomPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#FF01A4EF")
			};
		}
	}
}
