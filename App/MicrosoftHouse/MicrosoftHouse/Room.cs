using System;
using System.Windows.Input;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class Room : ViewModelBase
	{
		string name, piano,id;
		bool available;

		public Room()
		{
			RemoveCommand = new Command(() => RoomsBody.RemoveRoom(this));
			MoveToTopCommand = new Command(() => RoomsBody.MoveStudentToTop(this));
		}

		public string Name
		{
			set { SetProperty(ref name, value); }
			get { return name; }
		}

		public string Id
		{
			set { SetProperty(ref id, value); }
			get { return id; }
		}

		public string Piano
		{
			set { SetProperty(ref piano, value); }
			get { return piano; }
		}

		public bool Available
		{
			set { SetProperty(ref available, value); }
			get { return available; }
		}

		public ICommand RemoveCommand { private set; get; }
		public ICommand MoveToTopCommand { private set; get; }

		/*
		 * alla creazione di ogni oggetto ROOM viene assegnata la lista (RoomsBody) a cui appartiene
		 * room.RoomsBody = RoomsBody;
		 */
		public AllRoomsBody RoomsBody { set; get; }
	}
}
