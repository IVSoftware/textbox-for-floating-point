pathological - 
Lose focus with 1.2345678
Text changes to 1.23 but this is !Modified detect by comparing value to formatted
Gain focus with 1.23
Text changes to 1.2345678 = Still not modified because text matches undelying value





- Single control on form
- Validation event
- Set externally
- Revert



The accepted answer is simple and elegant, but seems suboptimal in the very likely scenario of the user hitting the Enter key (which basically does nothing) to commit the value. Using the built in validation of `TextBox` offers a more intuitive and common behavior where the value is formatted to two places, followed by a SelectAll. 






***

I tested the behavior with a minimal form.

[![focused entry][1]][1]

[![internal and external value set][2]][2]

***
Example of a custom textbox that uses [Textbox.Validating](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.validating?view=windowsdesktop-7.0) to meet these requirements:

    class TextBoxF2 : TextBox, INotifyPropertyChanged
    {
        public decimal Value    // The underlying value.
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }
        decimal _value = 0;
        public TextBoxF2()
        {
            CausesValidation = true;
            TextChanged += (sender, e) => _isDirty = true;
            Validating += (sender, e) =>
            {
                if (_isDirty && decimal.TryParse(Text, out decimal success))
                {
                    Value = success;
                }
            };
            GotFocus += (sender, e) => BeginInvoke(() => SelectAll());
            KeyDown += (sender, e) =>
            {   
                // Validate if the Enter key is pressed
                if (e.KeyData.Equals(Keys.Enter))
                {
                    e.SuppressKeyPress = e.Handled = true;
                    OnValidating(new CancelEventArgs());
                    BeginInvoke(() => SelectAll());
                }
            };
        }        
        private void onValueChanged()
        {   
            // Format value cause by keyboard or external value change.
            Text = Value.ToString("F2");
            _isDirty = false;
        }
        bool _isDirty = false;
        // Fire property changes when value changes
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            switch (propertyName)
            {
                case nameof(Value):
                    onValueChanged();
                    break;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

There are many ways to do what you're asking. Hopefully this moves you closer to an ideal solution.


  [1]: https://i.stack.imgur.com/SPbfz.png
  [2]: https://i.stack.imgur.com/QFaPC.png