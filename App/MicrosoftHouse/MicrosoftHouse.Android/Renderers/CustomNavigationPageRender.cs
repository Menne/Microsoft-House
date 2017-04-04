using System;
using MicrosoftHouse.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRender))]
namespace MicrosoftHouse.Droid
{
	public class CustomNavigationPageRender : NavigationRenderer
	{
		
	}
}
