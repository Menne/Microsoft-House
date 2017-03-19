using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.WindowsAzure.MobileServices;

namespace MicrosoftHouse
{
	public class AllRoomsViewModel : ViewModelBase
	{
		AllRoomsBody roomsBody;
		//IMobileServiceTable<Room> roomTable;
		//MobileServiceClient client;

		public AllRoomsViewModel()
		{
			//client = new MobileServiceClient("https://microsofthouse.azurewebsites.net");
			//roomTable = client.GetTable<Room>();


			RoomsBody = new AllRoomsBody();
			Room room;

			for (int i = 0; i < 10; i++)
			{
				room = new Room
				{
					Name = "Ciao",
					Available = true,
					Piano = "Piano 1"
				};

				//Set RoomsBody property in each Room object --> Necessario per il comando remove!
				/*
				 * La spiegazione sta nel fatto che ogni oggetto ROOM è mappato con un oggetto CELLA
				 * Quindi al momento del metodo del delete, abbiamo il reference all'oggetto ROOM, il quale per
				 * eliminarsi dalla lista a cui appartiene deve avere un reference alla lista che settiamo qua.
				 */
				room.RoomsBody = RoomsBody;
				RoomsBody.Rooms.Add(room);
			}

		}

		public AllRoomsBody RoomsBody
		{
			protected set { SetProperty(ref roomsBody, value); }
			get { return roomsBody; }
		}
	}
}

