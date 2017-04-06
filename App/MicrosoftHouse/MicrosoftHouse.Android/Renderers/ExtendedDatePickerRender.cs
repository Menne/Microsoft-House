using System;
using System.ComponentModel;
using MicrosoftHouse;
using MicrosoftHouse.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRender))]
namespace MicrosoftHouse.Droid
{
	public class ExtendedDatePickerRender : DatePickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);

			ExtendedDatePicker datePicker = (ExtendedDatePicker)Element;

			if (datePicker != null)
			{
				Control.SetBackgroundColor(Color.White.ToAndroid());
				//Control.FontFeatureSettings = 
				//Control.BorderStyle = UITextBorderStyle.None;
				//Control.Font = UIFont.FromName("Avenir", 14f);
				SetPlaceholder(datePicker);
				SetTextColor(datePicker);

			}

			if (e.OldElement == null)
			{
				//Wire events
			}

			if (e.NewElement == null)
			{
				//Unwire events
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
				SetTextColor(datePicker);
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
