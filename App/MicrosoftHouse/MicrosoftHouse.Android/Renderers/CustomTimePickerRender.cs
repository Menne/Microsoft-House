using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using MicrosoftHouse;
using MicrosoftHouse.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomTimePicker), typeof(CustomTimePickerRender))]
namespace MicrosoftHouse.Droid
{
	public class CustomTimePickerRender : TimePickerRenderer
	{
		//PUSH - Prova
		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);

			CustomTimePicker timePicker = (CustomTimePicker)Element;

			if (timePicker != null)
			{
				Control.TextSize = 14f;

				/*var shape = new ShapeDrawable(new RectShape());
				shape.Paint.Alpha = 0;
				shape.Paint.SetStyle(Paint.Style.Stroke);
				Control.SetBackgroundDrawable(shape);*/


				Control.Background.SetAlpha(10);
				Control.Background.SetColorFilter(Android.Graphics.Color.Black, PorterDuff.Mode.SrcAtop);

				//Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Avenir");  // font name specified here
				//Control.Typeface = font;
				//Control.Typeface =
				//Control.FontFeatureSettings = 
				//Control.BorderStyle = UITextBorderStyle.None;
				//Control.Font = UIFont.FromName("Avenir", 14f);
				SetPlaceholder(timePicker);
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

			CustomTimePicker timePicker = (CustomTimePicker)Element;

			if (e.PropertyName == CustomTimePicker.TextColorProperty.PropertyName)
			{
				this.Control.SetTextColor(timePicker.TextColor.ToAndroid());
			}
		}

		void SetTextColor(CustomTimePicker timePicker)
		{
			this.Control.SetTextColor(timePicker.TextColor.ToAndroid());
		}

		void SetPlaceholder(CustomTimePicker timePicker)
		{
			this.Control.Text = timePicker.PlaceHolder;
		}
	}
}
