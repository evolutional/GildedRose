using NUnit.Framework;
using GildedRose.Console;
using FluentAssertions;

namespace GildedRose.Tests
{
	[TestFixture]
	public class ItemTypingRules
	{
		private static readonly Console.GildedRose.Item _normalCheese = new Console.GildedRose.Item {Name = "Aged Brie"};
		private static readonly Console.GildedRose.Item _conjuredCheese = new Console.GildedRose.Item {Name = "Conjured Aged Brie"};
		private static readonly Console.GildedRose.Item _normalTickets = new Console.GildedRose.Item { Name = UpdatingQuality.TicketsName };
		private static readonly Console.GildedRose.Item _conjuredTickets = new Console.GildedRose.Item { Name = "Conjured " + UpdatingQuality.TicketsName };

		[Test]
		public void ShouldRecognizeBrieAsCheese()
		{
			_normalCheese.IsCheese().Should().BeTrue();
		}

		[Test]
		public void ShouldRecognizeConjuredBrieAsCheese()
		{
			_conjuredCheese.IsCheese().Should().BeTrue();
		}

		[Test]
		public void ShouldRecognizeBackstagePassesAsTickets()
		{
			_normalTickets.IsTickets().Should().BeTrue();
		}

		[Test]
		public void ShouldRecognizeConjuredBackstagePassesAsTickets()
		{
			_conjuredTickets.IsTickets().Should().BeTrue();
		}
	}
}
