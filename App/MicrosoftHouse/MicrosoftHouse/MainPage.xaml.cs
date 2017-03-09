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

		string translatedNumber;

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
