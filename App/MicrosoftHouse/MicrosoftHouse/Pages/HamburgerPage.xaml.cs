using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class HamburgerPage : ContentPage
	{
		public HamburgerPage()
		{
			InitializeComponent();
			BindingContext = new HamburgerViewModel();
		}
	}
}
