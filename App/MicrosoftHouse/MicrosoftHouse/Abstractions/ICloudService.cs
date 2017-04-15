using System;
using System.Threading.Tasks;

namespace MicrosoftHouse.Abstractions
{
	public interface ICloudService
	{
		ICloudTable<T> GetTable<T>() where T : TableData;

		Task LoginAsync();

		Task<AppServiceIdentity> GetIdentityAsync();

		Task LogoutAsync();

		// Custom Login
		//Task LoginAsync(User user);
	}
}
