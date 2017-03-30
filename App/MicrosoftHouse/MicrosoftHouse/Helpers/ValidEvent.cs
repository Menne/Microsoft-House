using System;
using Xamarin.Forms;

namespace MicrosoftHouse
{
	public class ValidEvent : Behavior<ExtendedEntry>
	{
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(ValidEvent), false);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public bool IsValid
		{
			private set { SetValue(IsValidPropertyKey, value); }
			get { return (bool)GetValue(IsValidProperty); }
		}

		protected override void OnAttachedTo(ExtendedEntry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}

		protected override void OnDetachingFrom(ExtendedEntry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
		}

		void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			ExtendedEntry entry = (ExtendedEntry)sender;
			IsValid = IsValidEvent(entry.Text);
		}
		bool IsValidEvent(string strIn)
		{
			if (String.IsNullOrEmpty(strIn))
				return false;
			else
				return true;
		}


	}
}
