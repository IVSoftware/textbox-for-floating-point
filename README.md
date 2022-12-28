The [accepted answer](https://stackoverflow.com/a/74894995/5438626) is simple and elegant. As I understand it, the scheme relies on changes to the focused state of the control, but if the user types some keys then hits the Enter key there's no guarantee that focus _will_ change. Another nice amenity would be to have a settable/bindable `Value` property that fires `PropertyChanged` events when a new valid value is received (either by keyboard input or programmatically).

In other words, this post just offers a few tweaks to an already excellent answer.

***
**Start with a bindable `Value` property for the undelying value**

Allows setting the underlying value programmatically e.g.  `textBoxFormatted.Value = 123.456789`.

    class TextBoxFP : TextBox, INotifyPropertyChanged
    {
        public TextBoxFP()
        {
            _unmodified = Text = "0.00";
            CausesValidation = true;
        }
        public double Value  
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    formatValue();
                    OnPropertyChanged();
                }
            }
        }
        double _value = 0;    
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

***
**Handle the Enter key**

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        switch (e.KeyData)
        {
            case Keys.Return:
                e.SuppressKeyPress = e.Handled = true;
                OnValidating(new CancelEventArgs());
                break;
            case Keys.Escape:
                e.SuppressKeyPress = e.Handled = true;
                formatValue();
                break;
        }
    }

![screenshot](https://github.com/IVSoftware/textbox-for-floating-point/blob/master/formatted-textbox/Screenshots/single.enter.png)




However, there are common use cases where `FormattedNumberTextBox` would benefit from a more conventional implementation of TextBox:

- Handle the [Enter] key (where control doesn't necessarily lose focus).
- Ability to set underlying floating point externally.
- Bindings that rely on `PropertyChange` notifications for changes to the underlying floating point value.
- Revert to last valid value on invalid input so as not to crash said bindings.

Here's a comparative analysis:

[![comparison][1]][1]

***
A more-fully implemented textbox that meets the original requirements.

    class TextBoxFP : TextBox, INotifyPropertyChanged
    {
        public TextBoxFP()
        {
            _unmodified = Text = "0.00";
            CausesValidation = true;
        }
        public double Value  
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    formatValue();
                    OnPropertyChanged();
                }
            }
        }
        double _value = 0;
        string _unmodified;
        public string Format { get; set; } = "N2";
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if(Focused)
            {
                Modified = !Text.Equals(_unmodified);
            }
        }
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            if (Modified)
            {
                if (double.TryParse(Text, out double value))
                {
                    Value = value;
                }
                formatValue();
                _unmodified = Text;
                Modified = false;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!(ReadOnly || Modified))
            {
                BeginInvoke(() =>
                {
                    int selB4 = SelectionStart;
                    Text = Value == 0 ? "0.00" : $"{Value}";
                    Modified = true;
                    Select(Math.Min(selB4, Text.Length - 1), 0);
                });
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyData)
            {
                case Keys.Return:
                    e.SuppressKeyPress = e.Handled = true;
                    OnValidating(new CancelEventArgs());
                    break;
                case Keys.Escape:
                    e.SuppressKeyPress = e.Handled = true;
                    formatValue();
                    break;
            }
        }
        private void formatValue()
        {
            Text = Value.ToString(Format);
            Modified = false;
            BeginInvoke(() => SelectAll());
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


  [1]: https://i.stack.imgur.com/NiaLd.png