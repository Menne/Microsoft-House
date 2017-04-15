using MicrosoftHouse.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using MicrosoftHouse;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace MicrosoftHouse.UWP
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            CustomDatePicker datePicker = (CustomDatePicker)Element;


            if (datePicker != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
                SetPlaceholder(datePicker);
                SetTextColor(datePicker);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            CustomDatePicker datePicker = (CustomDatePicker)Element;

            if (e.PropertyName == CustomDatePicker.TextColorProperty.PropertyName)
            {
                SetTextColor(datePicker);
            }
        }

        void SetTextColor(CustomDatePicker datePicker)
        {
            
        }

        void SetPlaceholder(CustomDatePicker datePicker)
        {
            
        }
    }
}
    