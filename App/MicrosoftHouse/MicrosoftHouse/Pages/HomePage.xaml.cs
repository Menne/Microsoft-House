using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace MicrosoftHouse
{
	public partial class HomePage : ContentPage
	{
		ZXingScannerPage scanPage;

		public HomePage()
		{
			InitializeComponent();
			BindingContext = new HomeViewModel();
		}



		/*async void OnPark(object sender, EventArgs e)
		{
			var customOverlay = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			var torch = new Button
			{
				Text = "Torch",
				TextColor = Color.White,
				FontFamily = "Avenir",
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("#FF01A4EF"),
				HeightRequest = 30,
				WidthRequest = 80,
				BorderRadius = 20
			};
			torch.Clicked += delegate
			{
				scanPage.ToggleTorch();
			};

			customOverlay.Children.Add(torch);

			scanPage = new ZXingScannerPage(customOverlay: customOverlay);
			scanPage.OnScanResult += (result) =>
			{
				scanPage.IsScanning = false;

				Device.BeginInvokeOnMainThread(() =>
				{
					Navigation.PopAsync();
					DisplayAlert("Scanned Barcode", result.Text, "OK");
				});
			};

			await Navigation.PushAsync(scanPage);

			//await Navigation.PushAsync(new CustomScanPage());
		}*/


	}
}
