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
	}
}
