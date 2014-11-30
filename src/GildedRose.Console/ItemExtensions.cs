using System.Net.NetworkInformation;

namespace ConsoleApp
{
    public static class ItemExtensions
    {
        private static void IncreaseQuality(this GildedRose.Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
        }

        private static bool IsAgedBrie(this GildedRose.Item item)
        {
            return item.Name == "Aged Brie";
        }

        private static bool IsBackstagePass(this GildedRose.Item item)
        {
            return item.Name == "Backstage passes to a TAFKAL80ETC concert";
        }

        public static void Update(this GildedRose.Item item)
        {
            if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                return;
            }

            if (!item.IsAgedBrie() && !item.IsBackstagePass())
            {
                if (item.Quality > 0)
                {
                    item.Quality = item.Quality - 1;
                }
            }
            else
            {
                item.IncreaseQuality();

                if (item.IsBackstagePass())
                {
                    if (item.SellIn < 11)
                    {
                        item.IncreaseQuality();
                    }

                    if (item.SellIn < 6)
                    {
                        item.IncreaseQuality();
                    }
                }
              
            }

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
                if (!item.IsAgedBrie())
                {
                    if (!item.IsBackstagePass())
                    {
                        if (item.Quality > 0)
                        {
                             item.Quality = item.Quality - 1;
                        }
                    }
                    else
                    {
                        item.Quality = 0;
                    }
                }
                else
                {
                    item.IncreaseQuality();
                }
            }
        }
    }
}