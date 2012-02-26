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
	}
}
