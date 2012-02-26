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

		public object[][] DecrementQualityData = new[]
			{
				new object[] {30, 29, false},
				new object[] {30, 28, true},
				new object[] {0, 0, false},
				new object[] {1, 0, true},
			};

		[Test, TestCaseSource("DecrementQualityData")]
		public void DecrementingNormalItemShouldReduceQualityByOne(int initialQuality, int finalQuality, bool isConjured)
		{
			var data = ItemWithQuality(initialQuality, isConjured);
			Console.GildedRose.DecrementQuality(data);
			data.Quality.Should().Be(finalQuality);
		}

		public object[][] IncrementQualityData = new[]
			{
				new object[] {30, 31, false},
				new object[] {50, 50, false},
			};

		[Test, TestCaseSource("IncrementQualityData")]
		public void IncrementingNormalItemShouldIncreaseQualityByOne(int initialQuality, int finalQuality, bool isConjured)
		{
			var data = ItemWithQuality(initialQuality, isConjured);
			Console.GildedRose.IncrementQuality(data);
			data.Quality.Should().Be(finalQuality);
		}

		private static Console.GildedRose.Item ItemWithQuality(int quality, bool isConjured)
		{
			return new Console.GildedRose.Item { Name = isConjured ? "Conjured something or other" : "Arbitrary item", Quality = quality };
		}
	}
}
