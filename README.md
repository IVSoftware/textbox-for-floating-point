The [accepted answer](https://stackoverflow.com/a/74894995/5438626) is simple and elegant. As I understand it, the scheme relies on changes to the focused state of the control, but if the user types some keys then hits the Enter key there's no guarantee that focus _will_ change. 

So, this post just makes a few tweaks to an already excellent answer by handling this case and also adding another nice amenity - a settable/bindable `Value` property that fires `PropertyChanged` events when a new valid value is received (either by keyboard input or programmatically).

***
**Start with a bindable `Value` property for the underlying value**

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

This method _also_ responds to an Escape key event by reverting to the last good formatted value.

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

***
**Define behavior for when `TextBox` is calls its built-in validation.**

This performs format + SelectAll. If the new input string can't be parsed it simply reverts to the previous valid state.

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
    
![screenshot](https://github.com/IVSoftware/textbox-for-floating-point/blob/master/formatted-textbox/Screenshots/single.validate.png)


  [1]: https://i.stack.imgur.com/NiaLd.png