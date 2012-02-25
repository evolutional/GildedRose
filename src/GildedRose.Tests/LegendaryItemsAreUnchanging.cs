using FluentAssertions;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
	[TestFixture]
	public class LegendaryItemsAreUnchanging
	{
		private const string LegendaryItemName = "Sulfuras, Hand of Ragnaros";

		[Test]
		public void LegendaryItemQualityShouldNotBeAdjustable()
		{
			var testSubject = new Console.GildedRose.Item {Name = LegendaryItemName, Quality = 80};
			testSubject.AdjustQuality(99);
			testSubject.Quality.Should().Be(80);
		}

		[Test]
		public void LegendaryItemQualityShouldNotBeSettabelToZero()
		{
			var testSubject = new Console.GildedRose.Item {Name = LegendaryItemName, Quality = 80};
			testSubject.SetQualityToZero();
			testSubject.Quality.Should().Be(80);
		}

		[Test]
		public void LegendaryItemsShouldNotAge()
		{
			var testSubject = new Console.GildedRose.Item {Name = LegendaryItemName, SellIn = 3};
			testSubject.AgeOneDay();
			testSubject.SellIn.Should().Be(3);
		}
	}
}
