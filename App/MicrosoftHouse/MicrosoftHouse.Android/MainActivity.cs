using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
//using MicrosoftHouse.Droid.Services;
using MicrosoftHouse.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Reflection;
using Android.Text.Style;
using Android.Text;

namespace MicrosoftHouse.Droid
{
	//, MainLauncher = true
    [Activity(Label = "MicrosoftHouse", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

			// AZURE
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			// MAPPE
            Xamarin.FormsMaps.Init(this, bundle);


            global::Xamarin.Forms.Forms.Init(this, bundle);

			// QR CODE
			ZXing.Net.Mobile.Forms.Android.Platform.Init();


            LoadApplication(new App());

			var spannableString = new SpannableString(SupportActionBar.Title);
			spannableString.SetSpan(new TypefaceSpan("Avenir"), 0, spannableString.Length(), SpanTypes.ExclusiveExclusive);
			SupportActionBar.TitleFormatted = spannableString;

        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

    }
}

