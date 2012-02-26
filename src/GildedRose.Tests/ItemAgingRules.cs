using System.Collections.Generic;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using System.Linq;

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
			for (var i = 0; i < 50; ++i)
			{
				trace.Add(FormatInventory(testSubject));
				testSubject.UpdateQuality();
			}
			trace.Add(FormatInventory(testSubject));
			Approvals.VerifyAll(trace, string.Empty);
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
