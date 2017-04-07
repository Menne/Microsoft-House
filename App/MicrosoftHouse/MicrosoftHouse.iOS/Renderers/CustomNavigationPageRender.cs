using System;
using MicrosoftHouse;
using MicrosoftHouse.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRender))]
namespace MicrosoftHouse.iOS.Renderer
{
	public class CustomNavigationPageRender : NavigationRenderer
	{
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (this.NavigationBar == null) return;


			this.NavigationBar.TintColor = Color.White.ToUIColor();

			this.NavigationBar.TitleTextAttributes = new UIStringAttributes
			{
				Font = UIFont.FromName("Avenir", 16f),
				ForegroundColor = UIKit.UIColor.White
			};

			//SetNavBarStyle();
			//SetNavBarTitle();
			//SetNavBarItems();
		}

		/*private void SetNavBarStyle()
		{
			NavigationBar.ShadowImage = new UIImage();
			NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
			UINavigationBar.Appearance.ShadowImage = new UIImage();
			UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
		}*/

		/*private void SetNavBarItems()
		{
			var navPage = this.Element as CustomNavPage;

			if (navPage == null) return;

			var textAttributes = new UITextAttributes()
			{
				Font = UIFont.FromName(navPage.BarItemFontFamily, navPage.BarItemFontSize)
			};

			var textAttributesHighlighted = new UITextAttributes()
			{
				TextColor = Color.Black.ToUIColor(),
				Font = UIFont.FromName(navPage.BarItemFontFamily, navPage.BarItemFontSize)
			};

			UIBarButtonItem.Appearance.SetTitleTextAttributes(textAttributes,
				UIControlState.Normal);
			UIBarButtonItem.Appearance.SetTitleTextAttributes(textAttributesHighlighted,
				UIControlState.Highlighted);
		}*/

		/*private void SetNavBarTitle()
		{
			var navPage = this.Element as CustomNavPage;

			if (navPage == null) return;

			this.NavigationBar.TitleTextAttributes = new UIStringAttributes
			{
				Font = UIFont.FromName(navPage.BarTitleFontFamily, 12f),
			};
		}*/
	}
}
