using System;

namespace GildedRose.Console
{
	public static class ItemExtensions
	{
		private const string _conjured = "Conjured ";

		public static void AdjustQuality(this GildedRose.Item item, int amount)
		{
			if (IsLegendary(item)) return;
			item.Quality = ComputeNewQuality(item.Quality, amount * (item.IsConjured() ? 2 : 1));
		}

		public static void SetQualityToZero(this GildedRose.Item item)
		{
			if (IsLegendary(item)) return;
			item.Quality = 0;
		}

		public static void AgeOneDay(this GildedRose.Item item)
		{
			if(!item.IsLegendary())
			{
				item.SellIn = item.SellIn - 1;
			}
		}

		public static bool IsTickets(this GildedRose.Item item)
		{
			return item.BaseName() == "Backstage passes to a TAFKAL80ETC concert";
		}

		public static bool IsCheese(this GildedRose.Item item)
		{
			return item.BaseName() == "Aged Brie";
		}

		private static string BaseName(this GildedRose.Item item)
		{
			return item.IsConjured() ? item.Name.Substring(_conjured.Length) : item.Name;
		}

		private static bool IsConjured(this GildedRose.Item item)
		{
			if (string.IsNullOrEmpty(item.Name)) return false;
			return item.Name.StartsWith(_conjured);
		}

		private static bool IsLegendary(this GildedRose.Item item)
		{
			return item.Name == "Sulfuras, Hand of Ragnaros";
		}

		private static int ComputeNewQuality(int initialQuality, int amount)
		{
			int quality = initialQuality + amount;
			quality = Math.Max(0, Math.Min(50, quality));
			return quality;
		}

		public static bool IsExpired(this GildedRose.Item item)
		{
			return item.SellIn < 0;
		}
	}
}
