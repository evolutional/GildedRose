using FluentAssertions;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
    [TestFixture]
    public class AgeTheInventory
    {
        [Test]
        public void Sulfuras_Update_does_not_change_in_quality()
        {
            var item = new ItemBuilder().WithName("Sulfuras, Hand of Ragnaros").WithQuality(80).WithSellIn(5).Build();
            item.Update();
            item.Quality.Should().Be(80);
            item.SellIn.Should().Be(5);
        }
    }
}