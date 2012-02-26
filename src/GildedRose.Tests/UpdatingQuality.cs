using FluentAssertions;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
	[TestFixture]
	public class UpdatingQuality
	{
		[Test]
		public void ItemQualityShouldAlwaysRemainWithinAllowedMax()
		{
			var testSubject = new Console.GildedRose.Item { Quality = 3 };
			testSubject.AdjustQuality(90);
			testSubject.Quality.Should().Be(50);
		}

		[Test]
		public void ItemQualityShouldAlwaysRemainWithinAllowedMin()
		{
			var testSubject = new Console.GildedRose.Item { Quality = 3 };
			testSubject.AdjustQuality(-90);
			testSubject.Quality.Should().Be(0);
		}

		[Test]
		public void ExpiredTicketsShouldGoToQualityZero()
		{
			var item = new Console.GildedRose.Item {Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 25};
			Console.GildedRose.UpdateQualityForExpiredItem(item).Quality.Should().Be(0);
		}

		[Test]
		public void ExpiredCheeseShouldGetMuchBetter()
		{
			var item = new Console.GildedRose.Item {Name = "Aged Brie", Quality = 25};
			Console.GildedRose.UpdateQualityForExpiredItem(item).Quality.Should().Be(27);
		}
	}
}
