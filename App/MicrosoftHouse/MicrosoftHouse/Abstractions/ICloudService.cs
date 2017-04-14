using System;
using System.Threading.Tasks;

namespace MicrosoftHouse.Abstractions
{
	public interface ICloudService
	{
		ICloudTable<T> GetTable<T>() where T : TableData;

		Task LoginAsync();

		Task<AppServiceIdentity> GetIdentityAsync();

		// Custom Login
		//Task LoginAsync(User user);
	}
}
