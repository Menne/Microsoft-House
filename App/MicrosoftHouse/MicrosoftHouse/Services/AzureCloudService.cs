using System;
using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse.Abstractions;

namespace MicrosoftHouse.Services
{
	public class AzureCloudService : ICloudService
	{
		MobileServiceClient client;

		public AzureCloudService()
		{
			client = new MobileServiceClient("https://microsofthouseofficial.azurewebsites.net");
		}

		public ICloudTable<T> GetTable<T>() where T : TableData
		{
			return new AzureCloudTable<T>(client);
		}
	}
}
