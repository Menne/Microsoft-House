using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.Abstractions;
using MicrosoftHouse.Models;
using Xamarin.Forms;

namespace MicrosoftHouse.ViewModels
{
	public class AllRoomsViewModel : BaseViewModel
	{
		public AllRoomsViewModel()
		{
			Title = "All Rooms";
			RefreshList();
		}

		ObservableCollection<Room> items = new ObservableCollection<Room>();
		public ObservableCollection<Room> Items
		{
			get { return items; }
			set { SetProperty(ref items, value, "Items"); }
		}

		Room selectedItem;
		public Room SelectedItem
		{
			get { return selectedItem; }
			set
			{
				SetProperty(ref selectedItem, value, "SelectedItem");
				if (selectedItem != null)
				{
					Application.Current.MainPage.Navigation.PushAsync(new RoomDetailPage(selectedItem));
					SelectedItem = null;
				}
			}
		}

		Command refreshCmd;
		public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

		async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				var table = App.CloudService.GetTable<Room>();
				var list = await table.ReadAllRoomsAsync();
				Items.Clear();
				foreach (var item in list)
					Items.Add(item);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[TaskList] Error loading items: {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}

		Command addNewCmd;
		public Command AddNewItemCommand => addNewCmd ?? (addNewCmd = new Command(async () => await ExecuteAddNewItemCommand()));

		async Task ExecuteAddNewItemCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
				await Application.Current.MainPage.Navigation.PushAsync(new RoomDetailPage());
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[TaskList] Error in AddNewItem: {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task RefreshList()
		{
			await ExecuteRefreshCommand();
			MessagingCenter.Subscribe<RoomDetailViewModel>(this, "ItemsChanged", async (sender) =>
			{
				await ExecuteRefreshCommand();
			});
		}
	}	
		

}

