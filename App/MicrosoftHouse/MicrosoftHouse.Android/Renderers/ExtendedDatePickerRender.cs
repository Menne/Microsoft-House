using System;
using MicrosoftHouse;
using MicrosoftHouse.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRender))]
namespace MicrosoftHouse.Droid
{
	public class ExtendedDatePickerRender : DatePickerRenderer
	{
		//PUSH - Prova
		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);

			//ExtendedDatePicker datePicker = (ExtendedDatePicker)Element;

			if (Control != null)
			{
				Control.Text = (Element as ExtendedDatePicker).PlaceHolder;
				Control.SetTextColor((Element as ExtendedDatePicker).TextColor.ToAndroid());
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control != null)
			{
				Control.Text = (Element as ExtendedDatePicker).PlaceHolder;
				Control.SetTextColor((Element as ExtendedDatePicker).TextColor.ToAndroid());
			}
		}

		/*void SetTextColor()
		{
			this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
		}

        void SetPlaceholder()
        {
            this.Control.Text = datePicker.PlaceHolder;
        }*/
    }
}
