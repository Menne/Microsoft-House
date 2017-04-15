using System;
using MicrosoftHouse;
using MicrosoftHouse.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRender))]
namespace MicrosoftHouse.iOS
{
	public class CustomEntryRender : EntryRenderer
	{
			/// <summary>
			/// The on element changed callback.
			/// </summary>
			/// <param name="e">The event arguments.</param>
			protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
			{
				base.OnElementChanged(e);

				var view = e.NewElement as CustomEntry;

				if (view != null)
				{
					Control.BorderStyle = UITextBorderStyle.None;

					SetFont(view);
					SetTextAlignment(view);
					SetPlaceholderTextColor(view);
					SetMaxLength(view);

					//ResizeHeight();
				}
			}

			/// <summary>
			/// The on element property changed callback
			/// </summary>
			/// <param name="sender">The sender.</param>
			/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
			protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
			{
				base.OnElementPropertyChanged(sender, e);

				var view = (CustomEntry)Element;

				if (e.PropertyName == CustomEntry.FontProperty.PropertyName)
					SetFont(view);
				if (e.PropertyName == CustomEntry.XAlignProperty.PropertyName)
					SetTextAlignment(view);
				if (e.PropertyName == CustomEntry.PlaceholderTextColorProperty.PropertyName)
					SetPlaceholderTextColor(view);

				//ResizeHeight();
			}

			/// <summary>
			/// Sets the text alignment.
			/// </summary>
			/// <param name="view">The view.</param>
			private void SetTextAlignment(CustomEntry view)
			{
				switch (view.XAlign)
				{
					case TextAlignment.Center:
						Control.TextAlignment = UITextAlignment.Center;
						break;
					case TextAlignment.End:
						Control.TextAlignment = UITextAlignment.Right;
						break;
					case TextAlignment.Start:
						Control.TextAlignment = UITextAlignment.Left;
						break;
				}
			}

			/// <summary>
			/// Sets the font.
			/// </summary>
			/// <param name="view">The view.</param>
			private void SetFont(CustomEntry view)
			{
				UIFont uiFont;
				if (view.Font != Font.Default && (uiFont = view.Font.ToUIFont()) != null)
					Control.Font = uiFont;
				else if (view.Font == Font.Default)
					Control.Font = UIFont.SystemFontOfSize(17f);
			}
			/// <summary>
			/// Sets the maxLength.
			/// </summary>
			/// <param name="view">The view.</param>
			private void SetMaxLength(CustomEntry view)
			{
				Control.ShouldChangeCharacters = (textField, range, replacementString) =>
				{
					var newLength = textField.Text.Length + replacementString.Length - range.Length;
					return newLength <= view.MaxLength;
				};
			}

			/// <summary>
			/// Resizes the height.
			/// </summary>
			private void ResizeHeight()
			{
				if (Element.HeightRequest >= 0) return;

				var height = Math.Max(Bounds.Height,
					new UITextField { Font = Control.Font }.IntrinsicContentSize.Height);

				Control.Frame = new CGRect(0.0f, 0.0f, (nfloat)Element.Width, (nfloat)height);

				Element.HeightRequest = height;
			}

			/// <summary>
			/// Sets the color of the placeholder text.
			/// </summary>
			/// <param name="view">The view.</param>
			void SetPlaceholderTextColor(CustomEntry view)
			{
				/*
	UIColor *color = [UIColor lightTextColor];
	YOURTEXTFIELD.attributedPlaceholder = [[NSAttributedString alloc] initWithString:@"PlaceHolder Text" attributes:@{NSForegroundColorAttributeName: color}];
				*/
				if (string.IsNullOrEmpty(view.Placeholder) == false && view.PlaceholderTextColor != Color.Default)
				{
					NSAttributedString placeholderString = new NSAttributedString(view.Placeholder, new UIStringAttributes() { ForegroundColor = view.PlaceholderTextColor.ToUIColor() });
					Control.AttributedPlaceholder = placeholderString;
				}
			}

	}
}