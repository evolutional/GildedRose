using NUnit.Framework;
using FluentAssertions;

namespace GildedRose.Tests
{
	[TestFixture]
	public class AdjustQuality
	{
		[Test]
		public void LegendaryItemShouldNeverLoseQuality()
		{
			var data = new Console.GildedRose.Item { Name = "Sulfuras, Hand of Ragnaros", Quality = 80 };
			Console.GildedRose.DecrementQuality(data);
			data.Quality.Should().Be(80);
		}

		[Test]
		public void DecrementingNormalItemShouldReduceQualityByOne()
		{
			var data = ItemWithQuality(30);
			Console.GildedRose.DecrementQuality(data);
			data.Quality.Should().Be(29);
		}

		[Test]
		public void DecrementingNormalItemShouldNeverReduceQualityBelowZero()
		{
			var data = ItemWithQuality(0);
			Console.GildedRose.DecrementQuality(data);
			data.Quality.Should().Be(0);
		}

		[Test]
		public void IncrementingNormalItemShouldIncreaseQualityByOne()
		{
			var data = ItemWithQuality(30);
			Console.GildedRose.IncrementQuality(data);
			data.Quality.Should().Be(31);
		}

		[Test]
		public void IncrementingNormalItemShouldNeverImproveQualityAboveFifty()
		{
			var data = ItemWithQuality(50);
			Console.GildedRose.IncrementQuality(data);
			data.Quality.Should().Be(50);
		}

		private static Console.GildedRose.Item ItemWithQuality(int quality)
		{
			return new Console.GildedRose.Item { Name = "Arbitrary item", Quality = quality };
		}
	}
}
