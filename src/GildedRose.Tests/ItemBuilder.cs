namespace ConsoleApp.Tests
{
    public class ItemBuilder
    {
        private bool _conjured;
        private string _name;
        private int _sellIn;
        private int _quality;

        public ItemBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ItemBuilder WithSellIn(int value)
        {
            _sellIn = value;
            return this;
        }

        public ItemBuilder WithQuality(int value)
        {
            _quality = value;
            return this;
        }

        public GildedRose.Item Build()
        {
            var modifiedName = _conjured ? "Conjured " + _name : _name;
            return new GildedRose.Item() { Name = modifiedName, SellIn = _sellIn, Quality = _quality};
        }

        public ItemBuilder WhenConjured()
        {
            _conjured = true;
            return this;
        }
    }
}