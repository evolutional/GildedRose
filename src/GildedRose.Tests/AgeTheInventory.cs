using FluentAssertions;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
    [TestFixture]
    public class AgeTheInventory
    {
        private ItemBuilder TestItem()
        {
            return new ItemBuilder().WithName("Non special test item");
        }
        private ItemBuilder BackstagePasses()
        {
            return new ItemBuilder().WithName("Backstage passes to a TAFKAL80ETC concert");
        }

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

        [Test]
        public void Conjured_AgedBrie_Update_increases_in_quality_twice_as_fast_as_non_conjured()
        {
            var item = AgedBrie().WhenConjured().WithQuality(10).WithSellIn(5).Build();
            item.Update();
            item.Quality.Should().Be(12);
        }

        [Test]
        public void Conjured_AgedBrie_when_SellIn_Passed_Update_double_increases_in_quality_over_non_conjured()
        {
            var item = AgedBrie().WhenConjured().WithQuality(10).WithSellIn(0).Build();
            item.Update();
            item.Quality.Should().Be(14);
        }

        [Test]
        public void BackstagePasses_when_SellIn_Over_10_days_Update_increases_in_quality()
        {
            var item = BackstagePasses().WithQuality(1).WithSellIn(15).Build();
            item.Update();
            item.Quality.Should().Be(2);
        }

        [Test]
        public void BackstagePasses_when_SellIn_Over_5_days_Update_increases_in_quality_twice_as_fast()
        {
            var item = BackstagePasses().WithQuality(1).WithSellIn(6).Build();
            item.Update();
            item.Quality.Should().Be(3);
        }

        [Test]
        public void BackstagePasses_when_SellIn_Under_5_days_Update_increases_in_quality_three_times_as_fast()
        {
            var item = BackstagePasses().WithQuality(1).WithSellIn(3).Build();
            item.Update();
            item.Quality.Should().Be(4);
        }

        [Test]
        public void BackstagePasses_when_SellIn_expired_Update_Then_quality_is_zero()
        {
            var item = BackstagePasses().WithQuality(20).WithSellIn(0).Build();
            item.Update();
            item.Quality.Should().Be(0);
        }


        [Test]
        public void Conjured_BackstagePasses_when_SellIn_Over_5_days_Update_increases_in_quality_twice_as_fast_as_non_conjured()
        {
            var item = BackstagePasses().WhenConjured().WithQuality(1).WithSellIn(6).Build();
            item.Update();
            item.Quality.Should().Be(5);
        }

        [Test]
        public void Conjured_BackstagePasses_when_SellIn_Under_5_days_Update_increases_in_quality_twice_as_fast_as_non_conjured()
        {
            var item = BackstagePasses().WhenConjured().WithQuality(1).WithSellIn(3).Build();
            item.Update();
            item.Quality.Should().Be(7);
        }

        [Test]
        public void Conjured_BackstagePasses_when_SellIn_Over_10_days_Update_increases_in_quality_twice_as_fast_as_non_conjured()
        {
            var item = BackstagePasses().WhenConjured().WithQuality(1).WithSellIn(15).Build();
            item.Update();
            item.Quality.Should().Be(3);
        }

        
        [Test]
        public void NormalItems_Update_Then_quality_decreases()
        {
            var item = TestItem().WithQuality(20).WithSellIn(5).Build();
            item.Update();
            item.Quality.Should().Be(19);
        }

        [Test]
        public void NormalItems_when_sellIn_expired_Update_Then_quality_decreases_twice_as_fast()
        {
            var item = TestItem().WithQuality(20).WithSellIn(0).Build();
            item.Update();
            item.Quality.Should().Be(18);
        }

        [Test]
        public void NormalItems_when_sellIn_expired_Update_Then_quality_never_negative()
        {
            var item = TestItem().WithQuality(1).WithSellIn(0).Build();
            item.Update();
            item.Quality.Should().Be(0);
        }

        [Test]
        public void Conjured_NormalItems_Update_Then_quality_decreases_twice_as_fast()
        {
            var item = TestItem().WhenConjured().WithQuality(20).WithSellIn(5).Build();
            item.Update();
            item.Quality.Should().Be(18);
        }

        [Test]
        public void Conjured_NormalItems_when_sellIn_expired_Update_Then_quality_decreases_twice_as_fast()
        {
            var item = TestItem().WhenConjured().WithQuality(20).WithSellIn(0).Build();
            item.Update();
            item.Quality.Should().Be(16);
        }
    }
}