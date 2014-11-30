using System.Net.NetworkInformation;

namespace ConsoleApp
{
    public static class ItemExtensions
    {
        private static void IncreaseQuality(this GildedRose.Item item, int modifier)
        {
            item.Quality = item.Quality + modifier;
            if (item.Quality > 50)
            {
                item.Quality = 50;
            }
        }

        private static void DecreaseQuality(this GildedRose.Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
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

            if (item.IsAgedBrie() || item.IsBackstagePass())
            {
                var modifier = 1;

                if (item.IsBackstagePass())
                {
                    if (item.SellIn < 6)
                    {
                        modifier = 3;
                    }
                    else if (item.SellIn < 11)
                    {
                        modifier = 2;
                    }
                }

                item.IncreaseQuality(modifier);
            }
            else
            {
                item.DecreaseQuality();
            }

            item.SellIn = item.SellIn - 1;

            if (item.SellIn >= 0)
            {
                return;
            }

            if (item.IsAgedBrie())
            {
                item.IncreaseQuality(1);
            }
            else
            {
                if (item.IsBackstagePass())
                {
                    item.Quality = 0;
                }
                else
                {
                    item.DecreaseQuality();
                }
            }
           
        }
    }
}