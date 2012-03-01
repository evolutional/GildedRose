namespace GildedRose.Console
{
	public static class ItemExtensions
	{
		public static bool IsTickets(this GildedRose.Item item)
		{
			return item.Name == "Backstage passes to a TAFKAL80ETC concert";
		}
	}
}