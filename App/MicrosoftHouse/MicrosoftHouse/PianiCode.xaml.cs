using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class PianiCode : ContentPage
	{
		public PianiCode()
		{
			InitializeComponent();

			Label label1 = new Label
			{
				Text = "Piano 1",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			Label label2 = new Label
			{
				Text = "Piano 2",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			Label label3 = new Label
			{
				Text = "Piano 3",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			Grid.SetRow(label1, 0);
			Grid.SetColumn(label1, 0);
			(Content as Grid).Children.Add(label1);
			Grid.SetRow(label2, 1);
			Grid.SetColumn(label2, 0);
			(Content as Grid).Children.Add(label2);
			Grid.SetRow(label3, 2);
			Grid.SetColumn(label3, 0);
			(Content as Grid).Children.Add(label3);

			//(Content as Grid).Children.Insert(0, grid);

                  
		}
	}
}
