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

namespace MicrosoftHouse.Droid
{
    [Activity(Label = "MicrosoftHouse", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //      XamForms.Controls.Droid.Calendar.Init();

            //		((DroidLoginProvider)DependencyService.Get<ILoginProvider>()).Init(this);

            LoadApplication(new App());
        }

    }
}

