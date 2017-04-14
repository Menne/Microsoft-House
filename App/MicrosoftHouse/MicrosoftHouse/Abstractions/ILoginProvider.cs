using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace MicrosoftHouse
{
	public interface ILoginProvider
	{
		Task LoginAsync(MobileServiceClient client);
	}
}
