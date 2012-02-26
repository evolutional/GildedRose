using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using NUnit.Framework;

namespace GildedRose.Tests
{
	[TestFixture, UseReporter(typeof(QuietReporter))]
	public class ItemAgingRules
	{
		[Test]
		public void ExistingBehaviorShouldRemainUnchanged()
		{
			var trace = new List<string>();
			var testSubject = new Console.GildedRose();
			for(int i = 0; i < 50; ++i)
			{
				trace.Add(FormatInventory(testSubject));
				testSubject.UpdateQuality();
			}
			trace.Add(FormatInventory(testSubject));
			Approvals.VerifyAll(trace, string.Empty);
		}

		[Test]
		public void ConjuredCheesShouldRotFaster()
		{
			var magicCheese = new Console.GildedRose.Item {Name = "Conjured Aged Brie", Quality = 30, SellIn = 3};
			Console.GildedRose.AgeOneDay(magicCheese);
			magicCheese.ShouldHave().SharedProperties().EqualTo(new { Quality = 32, SellIn = 2 });
		}

		private string FormatInventory(Console.GildedRose testSubject)
		{
			return "\r\n" + string.Join("\r\n", testSubject.Items.Select(FormatItem)) + "\r\n";
		}

		private string FormatItem(Console.GildedRose.Item item)
		{
			return string.Format("q={1}, sell={2}: {0}", item.Name, item.Quality, item.SellIn);
		}
	}
}
