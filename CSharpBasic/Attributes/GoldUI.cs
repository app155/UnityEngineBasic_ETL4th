namespace Attributes
{
    internal class GoldUI
    {
        [Bind("Value", SourceTag.Gold)]
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _text = _value.ToString();
            }
        }

        private int _value;

        public string Text => _text;

        private string _text;

        private Binder<GoldUI> _binder;

        public GoldUI()
        {
            _binder = new Binder<GoldUI>(this, GoldViewModel.Instance, SourceTag.Gold);
        }
    }
}
