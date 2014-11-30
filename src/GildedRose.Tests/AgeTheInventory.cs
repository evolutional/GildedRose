using FluentAssertions;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
    [TestFixture]
    public class AgeTheInventory
    {
        private ItemBuilder AgedBrie()
        {
            return new ItemBuilder().WithName("Aged Brie");
        }

        private ItemBuilder Sulfuras()
        {
            return new ItemBuilder().WithName("Sulfuras, Hand of Ragnaros");
        }

        [Test]
        public void Sulfuras_Update_does_not_change_in_quality()
        {
            var item = Sulfuras().WithQuality(80).WithSellIn(5).Build();
            item.Update();
            item.Quality.Should().Be(80);
        }

        [Test]
        public void Sulfuras_Update_does_not_change_sellIn()
        {
            var item = Sulfuras().WithSellIn(5).Build();
            item.Update();
            item.SellIn.Should().Be(5);
        }

        [Test]
        public void AgedBrie_Update_increases_in_quality()
        {
            var item = AgedBrie().WithQuality(10).WithSellIn(5).Build();
            item.Update();
            item.Quality.Should().Be(11);
        }

        [Test]
        public void AgedBrie_when_SellIn_Passed_Update_double_increases_in_quality()
        {
            var item = AgedBrie().WithQuality(10).WithSellIn(0).Build();
            item.Update();
            item.Quality.Should().Be(12);
        }

        [Test]
        public void AgedBrie_Update_quality_never_exceeds_50()
        {
            var item = AgedBrie().WithQuality(50).WithSellIn(5).Build();
            item.Update();
            item.Quality.Should().Be(50);
        }

    }
}