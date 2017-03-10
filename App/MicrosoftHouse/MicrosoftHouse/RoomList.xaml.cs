using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public partial class RoomList : ContentPage
	{

		/*TODO
		 * 1. Mettere in ordine le caselle,
		 * 2. Ogni Casella TAP Attivo per andare in un'altra pagina
		 */

		public RoomList()
		{
			InitializeComponent();

			StackLayout roomStack = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			StackLayout pianoStack = null;
			StackLayout rowStack = null;
			Frame pianoFrame = null;
			Label piano = null;

			for (int j = 1; j < 20; j++)
			{
				if ((j - 1) % 9 == 0)
				{
					pianoStack = new StackLayout();

					piano = new Label
					{
						Text = String.Concat("Piano ", j.ToString()),
						TextColor = Color.Black,
						FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
					};

					pianoStack.Children.Add(piano);

					pianoFrame = new Frame
					{
						Content = pianoStack,
						BackgroundColor = Color.Aqua
					};

					roomStack.Children.Add(pianoFrame);
				}


				if ((j - 1) % 4 == 0)
				{
					rowStack = new StackLayout
					{
						Orientation = StackOrientation.Horizontal
					};

					pianoStack.Children.Add(rowStack);
				}

				rowStack.Children.Add(CreateRoomView(true, "I.0.1"));

			}


			ScrollView scrollview = new ScrollView
			{
				Content = roomStack,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};

			(Content as StackLayout).Children.Insert(0, scrollview);

		}

		RoomView CreateRoomView(bool available, string name)
		{
			RoomView room = new RoomView();
			room.RoomName = name;
			room.Available = available;

			TapGestureRecognizer tapGesture = new TapGestureRecognizer();
			tapGesture.Tapped += OnRoomTapped;
			room.GestureRecognizers.Add(tapGesture);

			return room;
		}

		async void OnRoomTapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RoomPage());
		}



	}
}
