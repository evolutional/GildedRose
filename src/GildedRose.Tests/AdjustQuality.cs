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
				new object[] {30, 29},
				new object[] {0, 0},
			};

		[Test, TestCaseSource("DecrementQualityData")]
		public void DecrementingNormalItemShouldReduceQualityByOne(int initialQuality, int finalQuality)
		{
			var data = ItemWithQuality(initialQuality);
			Console.GildedRose.DecrementQuality(data);
			data.Quality.Should().Be(finalQuality);
		}

		public object[][] IncrementQualityData = new[]
			{
				new object[] {30, 31},
				new object[] {50, 50},
			};

		[Test, TestCaseSource("IncrementQualityData")]
		public void IncrementingNormalItemShouldIncreaseQualityByOne(int initialQuality, int finalQuality)
		{
			var data = ItemWithQuality(initialQuality);
			Console.GildedRose.IncrementQuality(data);
			data.Quality.Should().Be(finalQuality);
		}

		private static Console.GildedRose.Item ItemWithQuality(int quality)
		{
			return new Console.GildedRose.Item { Name = "Arbitrary item", Quality = quality };
		}
	}
}
