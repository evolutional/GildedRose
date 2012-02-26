using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
	[TestFixture, UseReporter(typeof(QuietReporter))]
	public class ItemAgingRules
	{
		[Test]
		public void LegacyShouldContinueToBehaveAsItAlwaysDid()
		{
			var testSubject = new Console.GildedRose();
			var trace = new StringBuilder();
			for(int i = 0; i < 50; ++i)
			{
				trace.AppendLine(DisplayInventory(testSubject.InventoryTestAccess));
				testSubject.UpdateQuality();
			}
			trace.AppendLine(DisplayInventory(testSubject.InventoryTestAccess));
			Approvals.Verify(trace.ToString());
		}

		[Test]
		public void ItemShouldExpireWhenSellInIsNegative()
		{
			new Console.GildedRose.Item {SellIn = -1}.IsExpired().Should().BeTrue();
		}

		[Test]
		public void ItemShouldnotBeExpiredOnLastDayOfSelling()
		{
			new Console.GildedRose.Item {SellIn = 0}.IsExpired().Should().BeFalse();
		}

		[Test]
		public void AgingShouldHappenBeforeQualityUpdate()
		{
			var ticketsOnLastDay = UpdatingQuality._Make(UpdatingQuality.TicketsName, 0);
			Console.GildedRose.AgeItem(ticketsOnLastDay);
			ticketsOnLastDay.ShouldHave().SharedProperties().EqualTo(new {SellIn = -1, Quality = 0});
		}

		private string DisplayInventory(IEnumerable<Console.GildedRose.Item> inventory)
		{
			return string.Join("\r\n", inventory.Select(DisplayItem)) + "\r\n";
		}

		private string DisplayItem(Console.GildedRose.Item it)
		{
			return string.Format("{0}, q={1}, sell in {2} days", it.Name, it.Quality, it.SellIn);
		}
	}
}
