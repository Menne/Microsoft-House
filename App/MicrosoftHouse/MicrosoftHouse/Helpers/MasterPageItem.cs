using System;
using MicrosoftHouse.Abstractions;

namespace MicrosoftHouse
{
	public class MasterPageItem : BaseViewModel
	{
		
		public string Name { get; set; }

		public string IconSource { get; set; }

		public Type TargetType { get; set; }

	}
}
