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

    //        InitializeStatistics();
        }

   /*     private void InitializeStatistics()
        {
            List<BoxView> barChart = new List<BoxView>();
            for (int i = 0; i < 13; i++)
            {
                BoxView boxView = new BoxView
                {
                    Color = Color.FromHex("FF01A4EF"),
                    HeightRequest = SelectedDayStatistics(i),
                    VerticalOptions = LayoutOptions.End,
                };
                boxView.SetBinding(HeightRequest)
                barChart.Add(boxView);
            }
            StatisticsGrid.Children.AddHorizontal(barChart);

            } 
        } */
    }
}
