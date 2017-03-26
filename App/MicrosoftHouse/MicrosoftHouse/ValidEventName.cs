using System;
using Xamarin.Forms;

namespace MicrosoftHouse
{
    public class ValidEventName : Behavior<Entry>
    {
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(ValidEventName), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsNotEmpty
        {
            private set { SetValue(IsValidPropertyKey, value); }
            get { return (bool)GetValue(IsValidProperty); }
        }

        public bool IsValidEventName
        {
            private set { SetValue(IsValidPropertyKey, value); }
            get { return (bool)GetValue(IsValidProperty); }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = (Entry)sender;
            IsNotEmpty = CheckIfNonEmptyEventName(entry.Text);
        }

        private bool CheckIfNonEmptyEventName(String eventName)
        {
            if (String.IsNullOrEmpty(eventName))
                return false;
            else
                return true;
        }
    }
}