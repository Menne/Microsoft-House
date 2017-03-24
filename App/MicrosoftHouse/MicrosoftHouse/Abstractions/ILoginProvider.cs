using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace MicrosoftHouse.Abstractions
{
	public interface ILoginProvider
	{
		Task LoginAsync(MobileServiceClient client);
	}
}
