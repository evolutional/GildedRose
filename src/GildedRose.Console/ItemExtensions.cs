namespace GildedRose.Console
{
	internal static class ItemExtensions
	{
		public static void DecrementQuality(this GildedRose.Item item)
		{
			if(item.Quality > 0)
			{
				AdjustQuality(item, -1);
			}
		}

		public static void IncrementQuality(this GildedRose.Item item)
		{
			if(item.Quality < 50)
			{
				AdjustQuality(item, 1);
			}
		}

		private static void AdjustQuality(GildedRose.Item item, int amount)
		{
			item.Quality = item.Quality + amount;
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
