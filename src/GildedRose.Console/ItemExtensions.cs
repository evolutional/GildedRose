using System;

namespace GildedRose.Console
{
	internal static class ItemExtensions
	{
		public static void DecrementQuality(this GildedRose.Item item)
		{
			if (IsLegendary(item)) return;
			item.Quality = ComputeNewQuality(item.Quality, -1);
		}

		public static void IncrementQuality(this GildedRose.Item item)
		{
			if (IsLegendary(item)) return;
			item.Quality = ComputeNewQuality(item.Quality, 1);
		}

		public static void SetQualityToZero(this GildedRose.Item item)
		{
			if (IsLegendary(item)) return;
			item.Quality = 0;
		}

		private static int ComputeNewQuality(int initialQuality, int amount)
		{
			int quality = initialQuality + amount;
			quality = Math.Max(0, Math.Min(50, quality));
			return quality;
		}

		private static bool IsLegendary(this GildedRose.Item item)
		{
			return item.Name == "Sulfuras, Hand of Ragnaros";
		}

		public static bool IsTickets(this GildedRose.Item item)
		{
			return item.Name == "Backstage passes to a TAFKAL80ETC concert";
		}

		public static bool IsCheese(this GildedRose.Item item)
		{
			return item.Name == "Aged Brie";
		}

		public static void AgeOneDay(this GildedRose.Item item)
		{
			if(!item.IsLegendary())
			{
				item.SellIn = item.SellIn - 1;
			}
		}
	}
}
