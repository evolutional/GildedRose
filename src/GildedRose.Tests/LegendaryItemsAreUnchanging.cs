using FluentAssertions;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
	[TestFixture]
	public class LegendaryItemsAreUnchanging
	{
		[Test]
		public void LegendaryItemQualityShouldNotBeAdjustable()
		{
			var testSubject = new Console.GildedRose.Item {Name = "Sulfuras, Hand of Ragnaros", Quality = 80};
			testSubject.AdjustQuality(99);
			testSubject.Quality.Should().Be(80);
		}

		[Test]
		public void LegendaryItemQualityShouldNotBeSettabelToZero()
		{
			var testSubject = new Console.GildedRose.Item {Name = "Sulfuras, Hand of Ragnaros", Quality = 80};
			testSubject.SetQualityToZero();
			testSubject.Quality.Should().Be(80);
		}

		[Test]
		public void LegendaryItemsShouldNotAge()
		{
			var testSubject = new Console.GildedRose.Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 3};
			testSubject.AgeOneDay();
			testSubject.SellIn.Should().Be(3);
		}
	}
}
