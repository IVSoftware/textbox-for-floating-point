The [accepted answer](https://stackoverflow.com/a/74894995/5438626) is simple and elegant. It states that it's the "easiest" way and I agree! But as I understand it, the scheme relies on changes to the focused state of the control so there's at least one "possible" issue: if the user types some keys then hits the Enter key there might not _be_ a change of focus (by design or default). 

So, this post just makes a few tweaks to an already excellent answer by handling [Enter] and adding another nice amenity - a settable/bindable `Value` property that fires `PropertyChanged` events when a new valid value is received (either by keyboard input or programmatically). At the same time, it ensures that when the textbox is `ReadOnly` it _always_ displays the formatted value.
    
[![screenshot focused entry or reentry][1]][1]
Focused entry or re-entry.

[![response to enter key][2]][2]
Response to Enter key

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


***
**Define behavior for when `TextBox` calls its built-in validation.**

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

***
**Ensure that a mouse click causes the full-resolution display:**

- Whether or not the control gains focus as a result.
- Only if control is _not_ read only.

_Use BeginInvoke which doesn't block remaining mouse events in queue._

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

***
**Implement the bindable `Value` property for the underlying value**

Allows setting the underlying value programmatically using  `textBoxFormatted.Value = 123.456789`.

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
**Manage the built-in `Modified` property of the text box and the actual formatting.**

    string _unmodified;
    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        if(Focused)
        {
            Modified = !Text.Equals(_unmodified);
        }
    }
    public string Format { get; set; } = "N2";
    private void formatValue()
    {
        Text = Value.ToString(Format);
        Modified = false;
        BeginInvoke(() => SelectAll());
    }

***
Testing

Bind the `PropertyChanged` event and attempt various valid (1.2345) and invalid ("abc") entries.

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBoxFormatted.PropertyChanged += (sender, e) =>
            {
                if(e.PropertyName == nameof(TextBoxFP.Value))
                {
                    textBoxBulk.Value = textBoxFormatted.Value * 100;
                    textBoxDiscount.Value = textBoxBulk.Value * - 0.10;
                    textBoxNet.Value = textBoxBulk.Value + textBoxDiscount.Value;
                }
            };
            buttonTestValue.Click += (sender, e) => textBoxFormatted.Value = (double)Math.PI;
        }
    }


  [1]: https://i.stack.imgur.com/3TJFE.png
  [2]: https://i.stack.imgur.com/hU3Ul.png