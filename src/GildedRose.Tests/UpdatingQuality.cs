using FluentAssertions;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
	[TestFixture]
	public class UpdatingQuality
	{
		private const string TicketsName = "Backstage passes to a TAFKAL80ETC concert";
		private static readonly Console.GildedRose.Item _cheese = _Make("Aged Brie");
		private static readonly Console.GildedRose.Item _normalItem = _Make("Some random BS item");
		private static readonly Console.GildedRose.Item _uncommonTickets = _Make(TicketsName, 8);
		private static readonly Console.GildedRose.Item _tickets = _Make(TicketsName, 15);
		private static readonly Console.GildedRose.Item _rareTickets = _Make(TicketsName, 1);

		[Test]
		public void ItemQualityShouldAlwaysRemainWithinAllowedMax()
		{
			var testSubject = new Console.GildedRose.Item {Quality = 3};
			testSubject.AdjustQuality(90);
			testSubject.Quality.Should().Be(50);
		}

		[Test]
		public void ItemQualityShouldAlwaysRemainWithinAllowedMin()
		{
			var testSubject = new Console.GildedRose.Item {Quality = 3};
			testSubject.AdjustQuality(-90);
			testSubject.Quality.Should().Be(0);
		}

		[Test, TestCaseSource("ItemQualityAgingCases")]
		public void ExpiredItemsShouldUpdateQualityCorrectly(
			string testCase, Console.GildedRose.Item item, int expectedFreshChange, int expectedExpiredChange)
		{
			int originalQuality = item.Quality;
			Console.GildedRose.UpdateQualityForExpiredItem(item).Quality.Should().Be(originalQuality + expectedExpiredChange);
		}

		[Test, TestCaseSource("ItemQualityAgingCases")]
		public void FreshItemsShouldUpdateQualityCorrectly(
			string testCase, Console.GildedRose.Item item, int expectedFreshChange, int expectedExpiredChange)
		{
			int originalQuality = item.Quality;
			Console.GildedRose.UpdateQualityForFreshItem(item).Quality.Should().Be(originalQuality + expectedFreshChange);
		}

		private static object[][] ItemQualityAgingCases = new[]
			{
				new object[] {"tickets", _tickets, 1, -25},
				new object[] {"uncommon tickets", _uncommonTickets, 2, -25},
				new object[] {"rare tickets", _rareTickets, 3, -25},
				new object[] {"cheese", _cheese, 1, 2},
				new object[] {"normal item", _normalItem, -1, -2},
			};

		private static Console.GildedRose.Item _Make(string name, int sellIn = 0)
		{
			return new Console.GildedRose.Item { Name = name, Quality = 25, SellIn = sellIn };
		}
	}
}
