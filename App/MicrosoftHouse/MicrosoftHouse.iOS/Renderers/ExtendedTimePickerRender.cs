using MicrosoftHouse;
using MicrosoftHouse.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRendererAttribute(typeof(ExtendedTimePicker), typeof(ExtendedTimePickerRender))]
namespace MicrosoftHouse.iOS
{
	public class ExtendedTimePickerRender : TimePickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);

			ExtendedTimePicker timePicker = (ExtendedTimePicker)Element;

			if (timePicker != null)
			{
				//TOGLIE IL BORDO 
				Control.BorderStyle = UITextBorderStyle.None;
				//FONT
				Control.Font = UIFont.FromName("Avenir", 14f);
				SetPlaceholder(timePicker);
				//SetFont(timePicker);
				SetTextColor(timePicker);
			}

		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control == null)
			{
				return;
			}

			ExtendedTimePicker timePicker = (ExtendedTimePicker)Element;
			if (e.PropertyName == ExtendedTimePicker.FontProperty.PropertyName)
			{
				SetFont(timePicker);
			}
			if (e.PropertyName == ExtendedTimePicker.TextColorProperty.PropertyName)
			{
				this.Control.TextColor = timePicker.TextColor.ToUIColor();
			}
		}

		// Setta il FONT_SIZE
		private void SetFont(ExtendedTimePicker timePicker)
		{
			UIFont uiFont;
			if (timePicker.Font != Font.Default && (uiFont = timePicker.Font.ToUIFont()) != null)
				Control.Font = uiFont;
			else if (timePicker.Font == Font.Default)
				Control.Font = UIFont.SystemFontOfSize(17f);
		}

		void SetTextColor(ExtendedTimePicker timePicker)
		{
			this.Control.TextColor = timePicker.TextColor.ToUIColor();
		}

		void SetPlaceholder(ExtendedTimePicker timePicker)
		{
			this.Control.Text = timePicker.PlaceHolder;
		}
		
	}
}
