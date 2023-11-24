namespace Attributes
{
    internal class GoldUI
    {
        [Bind("Value", SourceTag.Gold)]
        public string Text
        {
            get;
            private set;
        }
    }
}
