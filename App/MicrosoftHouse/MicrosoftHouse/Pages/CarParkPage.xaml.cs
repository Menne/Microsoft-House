using MicrosoftHouse.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class CarParkPage : ContentPage
	{
		public CarParkPage()
		{
			InitializeComponent();
            BindingContext = new CarParkViewModel();
        }

        public void OnPressed(object sender, EventArgs e)
        {
            foreach (Button button in DaysOfWeekButtonsGrid.Children)
            {
                button.BackgroundColor = Color.Transparent;
                button.TextColor = Color.Gray;
            }

            (sender as Button).BackgroundColor = Color.FromHex("#FF01A4EF");
            (sender as Button).TextColor = Color.White;
        }
    }
}
