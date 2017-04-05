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

			ExtendedDatePicker datePicker = (ExtendedDatePicker)Element;

			if (datePicker != null)
			{
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

			ExtendedDatePicker datePicker = (ExtendedDatePicker)Element;

			if (e.PropertyName == ExtendedDatePicker.TextColorProperty.PropertyName)
			{
				this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
			}
		}

		void SetTextColor(ExtendedDatePicker datePicker)
		{
			this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
		}

        void SetPlaceholder(ExtendedDatePicker datePicker)
        {
            this.Control.Text = datePicker.PlaceHolder;
        }
    }
}
