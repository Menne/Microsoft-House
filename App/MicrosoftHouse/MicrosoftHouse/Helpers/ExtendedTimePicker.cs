using System;
using MicrosoftHouse;
using Xamarin.Forms;


namespace MicrosoftHouse
{
	public class ExtendedTimePicker : TimePicker
	{
		public static readonly BindableProperty TextColorProperty = 
			BindableProperty.Create ("TextColor", typeof(Color), typeof(ExtendedTimePicker), Color.Default);

        public Color TextColor {
            get { return (Color)GetValue (TextColorProperty); }
            set { SetValue (TextColorProperty, value); }
        }

		/// <summary>
		/// The font property
		/// </summary>
		public static readonly BindableProperty FontProperty =
			BindableProperty.Create("Font", typeof(Font), typeof(ExtendedTimePicker), new Font());

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
		   typeof(ExtendedTimePicker),
		   "");

		public string PlaceHolder
		{
			get
			{
				return (string)GetValue(PlaceHolderProperty);
			}
			set
			{
				SetValue(PlaceHolderProperty, value);
			}
		}
	}
}
