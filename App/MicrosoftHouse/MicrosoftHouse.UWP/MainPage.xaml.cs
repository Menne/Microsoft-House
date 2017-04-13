using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MicrosoftHouse.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new MicrosoftHouse.App());

            ZXing.Net.Mobile.Forms.WindowsUniversal.ZXingScannerViewRenderer.Init();
            Xamarin.FormsMaps.Init("Nk9QD9nRpPnIGX5mYghH~sI1xsuHgE4DYdtR2I-NF_g~AmQQ3qbR6K0licmbqoStpvoY9JpeCY7pPvGgD42MnO-CQJ87j6I5DBOB_7FLk9pD");
        }
    }
}
