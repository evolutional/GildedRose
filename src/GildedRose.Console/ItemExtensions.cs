namespace GildedRose.Console
{
	internal static class ItemExtensions
	{
		public static void DecrementQuality(this GildedRose.Item item)
		{
			item.Quality = ComputeNewQualityDecrement(item);
		}

		public static void IncrementQuality(this GildedRose.Item item)
		{
			item.Quality = ComputeNewQualityIncrement(item);
		}

		private static int ComputeNewQualityDecrement(GildedRose.Item item)
		{
			int quality = item.Quality;
			if(item.Quality > 0)
			{
				quality = item.Quality + -1;
			}
			return quality;
		}

		private static int ComputeNewQualityIncrement(GildedRose.Item item)
		{
			int newQuality = item.Quality;
			if(item.Quality < 50)
			{
				newQuality = item.Quality + 1;
			}
			return newQuality;
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
