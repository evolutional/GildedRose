internal static class ItemExtensions
{
	public static void DecrementQuality(GildedRose.Console.GildedRose.Item item)
	{
		if(item.Quality > 0)
		{
			item.Quality = item.Quality - 1;
		}
	}

	public static void IncrementQuality(GildedRose.Console.GildedRose.Item item)
	{
		if(item.Quality < 50)
		{
			item.Quality = item.Quality + 1;
		}
	}

	public static bool IsLegendary(GildedRose.Console.GildedRose.Item item)
	{
		return item.Name == "Sulfuras, Hand of Ragnaros";
	}

	public static bool IsTickets(GildedRose.Console.GildedRose.Item item)
	{
		return item.Name == "Backstage passes to a TAFKAL80ETC concert";
	}

	public static bool IsCheese(GildedRose.Console.GildedRose.Item item)
	{
		return item.Name == "Aged Brie";
	}
}
