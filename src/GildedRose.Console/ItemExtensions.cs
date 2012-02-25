using System;

namespace GildedRose.Console
{
	internal static class ItemExtensions
	{
		public static void DecrementQuality(this GildedRose.Item item)
		{
			item.Quality = ComputeNewQualityDecrement(item.Quality);
		}

		public static void IncrementQuality(this GildedRose.Item item)
		{
			item.Quality = ComputeNewQualityIncrement(item.Quality);
		}

		private static int ComputeNewQualityDecrement(int initialQuality)
		{
			int quality = initialQuality + -1;
			quality = Math.Max(0, Math.Min(50, quality));
			return quality;
		}

		private static int ComputeNewQualityIncrement(int initialQuality)
		{
			int quality = initialQuality + 1;
			quality = Math.Max(0, Math.Min(50, quality));
			return quality;
		}

		public static bool IsLegendary(this GildedRose.Item item)
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

		public static void SetQualityToZero(this GildedRose.Item item)
		{
			item.Quality = 0;
		}
	}
}
