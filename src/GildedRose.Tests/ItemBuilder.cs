namespace ConsoleApp.Tests
{
    public class ItemBuilder
    {
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
            return new GildedRose.Item() { Name = _name, SellIn = _sellIn, Quality = _quality};
        }
    }
}