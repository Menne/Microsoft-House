using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class MasterPage : MasterDetailPage
	{
		public MasterPage()
		{
			InitializeComponent();

			hamburgerPage.ListView.ItemSelected += OnItemSelected;
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;
			if (item != null)
			{
				Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
				hamburgerPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}
