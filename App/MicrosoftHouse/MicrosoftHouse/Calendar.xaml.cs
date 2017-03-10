using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class Calendar : ContentPage
	{
		CalendarView calendarView;


		public Calendar()
		{
			InitializeComponent();

			calendarView = new CalendarView{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			calendarView.DateSelected += (object sender, DateTime e) =>
			{
				_stacker.Children.Add(new Label()
				{
					Text = "Date Was Selected" + e.ToString("d"),
					VerticalOptions = LayoutOptions.Start,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
				});
			};

			(Content as StackLayout).Children.Insert(0, calendarView);


		}
	}
}
