using MicrosoftHouse.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using MicrosoftHouse;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace MicrosoftHouse.UWP
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
            }

        }
    }
}
