using System;
using MicrosoftHouse;
using MicrosoftHouse.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRendererAttribute(typeof(ExtendedPicker), typeof(ExtendedPickerRender))]
namespace MicrosoftHouse.iOS
{
	public class ExtendedPickerRender : PickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			ExtendedPicker picker = (ExtendedPicker)Element;

			if (picker != null)
			{
				//TOGLIE IL BORDO 
				Control.BorderStyle = UITextBorderStyle.None;
				Control.Font = UIFont.FromName("Avenir", 14f);
				SetPlaceholder(picker);
				//SetFont(datePicker);
				SetTextColor(picker);
			}

		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control == null)
			{
				return;
			}

			ExtendedPicker picker = (ExtendedPicker)Element;

			if (e.PropertyName == ExtendedPicker.FontProperty.PropertyName)
			{
				SetFont(picker);
			}
				if (e.PropertyName == ExtendedPicker.TextColorProperty.PropertyName)
			{
				this.Control.TextColor = picker.TextColor.ToUIColor();
			}
		}

		// Setta il FONT_SIZE
		private void SetFont(ExtendedPicker picker)
		{
			UIFont uiFont;
			if (picker.Font != Font.Default && (uiFont = picker.Font.ToUIFont()) != null)
			Control.Font = uiFont;
			else if (picker.Font == Font.Default)
			Control.Font = UIFont.SystemFontOfSize(17f);
		}

		void SetTextColor(ExtendedPicker picker)
		{
			this.Control.TextColor = picker.TextColor.ToUIColor();
		}

		void SetPlaceholder(ExtendedPicker picker)
		{
			this.Control.Text = picker.PlaceHolder;
		}
	}
}
