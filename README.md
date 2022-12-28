The [accepted answer](https://stackoverflow.com/a/74894995/5438626) is _easiest_ (as advertised) and is simple, elegant and, well, accepted. 

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