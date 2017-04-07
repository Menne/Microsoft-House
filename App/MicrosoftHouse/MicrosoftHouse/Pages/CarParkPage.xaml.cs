using MicrosoftHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class CarParkPage : ContentPage
	{
		public CarParkPage()
		{
			InitializeComponent();
            BindingContext = new CarParkViewModel();

            InitializeStatistics();
        }

        private void InitializeStatistics()
        {
       /*     for (int i = 0; i < 12; i++)
            {
                BoxView boxView = new BoxView
                {
                    Color = Color.FromHex("FF01A4EF"),
                    VerticalOptions = LayoutOptions.End,
                };
                boxView.SetBinding(HeightRequestProperty, new Binding("SelectedDayStatistics", BindingMode.TwoWay, source: BindingContext));
                StatisticsGrid.Children.Add(boxView, i + 1, 0);
            }    */

            for (int i = 0; i < 13; i++)
            {
                Label label = new Label
                {
                    Text = (i+8).ToString(),
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                };
                StatisticsGrid.Children.Add(label, i, 1);
                Grid.SetColumnSpan(label, 2);
            }
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
