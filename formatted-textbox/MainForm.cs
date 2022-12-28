using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace formatted_textbox
{
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
}