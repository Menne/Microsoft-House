using MicrosoftHouse.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class CarPark : ContentPage
	{
		public CarPark()
		{
			InitializeComponent();
            BindingContext = new CarParkViewModel();
        }
	}
}
