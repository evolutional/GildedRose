using NUnit.Framework;
using GildedRose.Console;
using FluentAssertions;

namespace GildedRose.Tests
{
	[TestFixture]
	public class ItemTypingRules
	{
		[Test]
		public void ShouldRecognizeBrieAsCheese()
		{
			new Console.GildedRose.Item {Name = "Aged Brie"}.IsCheese().Should().BeTrue();
		}

		[Test]
		public void ShouldRecognizeBackstagePassesAsTickets()
		{
			new Console.GildedRose.Item { Name = "Backstage passes to a TAFKAL80ETC concert" }.IsTickets().Should().BeTrue();
		}
	}
}
