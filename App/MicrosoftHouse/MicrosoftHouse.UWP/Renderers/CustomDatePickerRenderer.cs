using MicrosoftHouse.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using MicrosoftHouse;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(CustomDatePickerRenderer))]
namespace MicrosoftHouse.UWP
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
            }

        }
    }
}
    