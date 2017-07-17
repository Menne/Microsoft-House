using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Helpers;
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

		public ICloudService CloudService => ServiceLocator.Get<ICloudService>();

		public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;

			if (item != null)
			{
				if (item.Name.Equals("Logout"))
				{
					 ExecuteLogout();
					return;
				}

				Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType))
				{
					BarTextColor = Color.White,
					BarBackgroundColor = Color.FromHex("#FF01A4EF")
				};
				hamburgerPage.ListView.SelectedItem = null;
                if (Device.Idiom == TargetIdiom.Desktop)
                {
                    IsPresented = true;
                }
                else
                {
                    IsPresented = false;
                }
            }
		}

		async Task ExecuteLogout()
		{
            var answer = await Application.Current.MainPage.DisplayAlert("Are you sure?", "We hope to see you soon", "Yes", "No");
            if (!answer)
                return;

            try
			{
				await CloudService.LogoutAsync();
				Application.Current.MainPage = new EntryPage();
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Logout Failed", ex.Message, "OK");
			}
		}
	}
}
