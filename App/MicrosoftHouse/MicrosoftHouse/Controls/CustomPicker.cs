using System;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class CustomPicker : Picker
	{
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create("TextColor", typeof(Color), typeof(CustomPicker), Color.Default);

		public Color TextColor
		{
		get { return (Color)GetValue(TextColorProperty); }
		set { SetValue(TextColorProperty, value); }
		}

		/// <summary>
		/// The font property
		/// </summary>
		public static readonly BindableProperty FontProperty =
			BindableProperty.Create("Font", typeof(Font), typeof(CustomPicker), new Font());

		/// <summary>
		/// Gets or sets the Font
		/// </summary>
		public Font Font
		{
		get { return (Font)GetValue(FontProperty); }
		set { SetValue(FontProperty, value); }
		}

		public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(
		   "PlaceHolder",
		   typeof(string),
		   typeof(CustomPicker),
		   "");

		public string PlaceHolder
		{
			get { return (string)GetValue(PlaceHolderProperty); }
			set { SetValue(PlaceHolderProperty, value); }
		}
	}
}
