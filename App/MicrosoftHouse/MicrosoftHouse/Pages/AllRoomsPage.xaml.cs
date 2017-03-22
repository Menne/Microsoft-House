using System;
using System.Collections.Generic;
using MicrosoftHouse.ViewModels;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class AllRoomsPage : ContentPage
	{
		public AllRoomsPage()
		{
			InitializeComponent();
			BindingContext = new AllRoomsViewModel();
		}
	}
}
