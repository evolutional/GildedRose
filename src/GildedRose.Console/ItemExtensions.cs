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
			if(quality < 0)
			{
				quality = 0;
			}
			return quality;
		}

		private static int ComputeNewQualityIncrement(int initialQuality)
		{
			int newQuality = initialQuality + 1;
			if(initialQuality > 50)
			{
				newQuality = 50;
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
