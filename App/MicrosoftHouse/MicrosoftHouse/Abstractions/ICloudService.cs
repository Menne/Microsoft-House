using System;
using System.Threading.Tasks;

namespace MicrosoftHouse.Abstractions
{
	public interface ICloudService
	{
		ICloudTable<T> GetTable<T>() where T : TableData;

	}
}
