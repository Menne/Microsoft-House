using System;
using MicrosoftHouse;
using MicrosoftHouse.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedTimePicker), typeof(ExtendedDatePickerRender))]
namespace MicrosoftHouse.Droid
{
	public class ExtendedTimePickerRender : TimePickerRenderer
	{
		//PUSH - Prova
		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);

			ExtendedTimePicker timePicker = (ExtendedTimePicker)Element;

			if (timePicker != null)
			{
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

			if (e.PropertyName == ExtendedTimePicker.TextColorProperty.PropertyName)
			{
				this.Control.SetTextColor(timePicker.TextColor.ToAndroid());
			}
		}

		void SetTextColor(ExtendedTimePicker timePicker)
		{
			this.Control.SetTextColor(timePicker.TextColor.ToAndroid());
		}
	}
}
