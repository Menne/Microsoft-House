using System;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class MainPage : ContentPage
	{

		string translatedNumber;

		public MainPage()
		{
			InitializeComponent();
		}

		void Translate(object sender, EventArgs e)
		{
			translatedNumber = phoneNumberText.Text;
			if (!string.IsNullOrWhiteSpace(translatedNumber))
			{
				callButton.IsEnabled = true;
				callButton.Text = "Call " + translatedNumber;
			}
			else
			{
				callButton.IsEnabled = false;
				callButton.Text = "Call";
			}
		}

		async void OnCallHistory(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SecondaryPage());
		}
	}
}
