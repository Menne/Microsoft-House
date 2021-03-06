﻿using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using MicrosoftHouse;
using MicrosoftHouse.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace MicrosoftHouse.Droid
{
	public class CustomDatePickerRenderer : DatePickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);

            CustomDatePicker datePicker = (CustomDatePicker)Element;

			if (datePicker != null)
			{
				//Control.SetBackgroundColor(Android.Graphics.Color.White);
				Control.TextSize = 14f;

				/*var shape = new ShapeDrawable(new RectShape());
				shape.Paint.Alpha = 0;
				shape.Paint.SetStyle(Paint.Style.Stroke);
				Control.SetBackgroundDrawable(shape);*/

				//Border
				Control.Background.SetAlpha(10);
				Control.Background.SetColorFilter(Android.Graphics.Color.Black, PorterDuff.Mode.SrcAtop);

				//Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Avenir");  // font name specified here
				//Control.Typeface = font;
				//Control.Typeface =
				//Control.FontFeatureSettings = 
				//Control.BorderStyle = UITextBorderStyle.None;
				//Control.Font = UIFont.FromName("Avenir", 14f);
				SetPlaceholder(datePicker);
				SetTextColor(datePicker);
				//SetFont(datePicker);

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

            CustomDatePicker datePicker = (CustomDatePicker)Element;

			if (e.PropertyName == CustomDatePicker.TextColorProperty.PropertyName)
			{
				SetTextColor(datePicker);
			}
		}

		void SetTextColor(CustomDatePicker datePicker)
		{
			this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
		}

		void SetPlaceholder(CustomDatePicker datePicker)
		{
			this.Control.Text = datePicker.PlaceHolder;
		}

		/*private void SetFont(ExtendedDatePicker datePicker)
		{
			if (datePicker.Font != Font.Default)
			{
				Control.TextSize = datePicker.Font.ToScaledPixel();
				//Control.Typeface = view.Font.ToExtendedTypeface(Context);
			}
		}*/
    }
}
