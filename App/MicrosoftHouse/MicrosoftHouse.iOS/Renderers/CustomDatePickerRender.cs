using System;
using MicrosoftHouse;
using MicrosoftHouse.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRendererAttribute(typeof(CustomDatePicker), typeof(CustomDatePickerRender))]
namespace MicrosoftHouse.iOS
{
	public class CustomDatePickerRender : DatePickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);

            CustomDatePicker datePicker = (CustomDatePicker)Element;

			if (datePicker != null)
			{
				//TOGLIE IL BORDO 
				Control.BorderStyle = UITextBorderStyle.None;
				Control.Font = UIFont.FromName("Avenir", 14f);
				SetPlaceholder(datePicker);
				//SetFont(datePicker);
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
			if (e.PropertyName == CustomDatePicker.FontProperty.PropertyName)
			{
				SetFont(datePicker);
			}
			if (e.PropertyName == CustomDatePicker.TextColorProperty.PropertyName)
			{
				this.Control.TextColor = datePicker.TextColor.ToUIColor();
			}
		}

		// Setta il FONT_SIZE
		private void SetFont(CustomDatePicker datePicker)
		{
			UIFont uiFont;
			if (datePicker.Font != Font.Default && (uiFont = datePicker.Font.ToUIFont()) != null)
				Control.Font = uiFont;
			else if (datePicker.Font == Font.Default)
				Control.Font = UIFont.SystemFontOfSize(17f);
		}

		void SetTextColor(CustomDatePicker datePicker)
		{
			this.Control.TextColor = datePicker.TextColor.ToUIColor();
		}

		void SetPlaceholder(CustomDatePicker datePicker)
		{
			this.Control.Text = datePicker.PlaceHolder;
		}
	}
}
