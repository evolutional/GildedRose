using System.Net.NetworkInformation;

namespace ConsoleApp
{
    public static class ItemExtensions
    {
        private static void ChangeQuality(this GildedRose.Item item, int modifier)
        {
            item.Quality = item.Quality + modifier;
            if (item.Quality > 50)
            {
                item.Quality = 50;
            }
            if (item.Quality < 0)
            {
                item.Quality = 0;
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

        private static void UpdateExpiredItem(this GildedRose.Item item)
        {
            if (item.IsBackstagePass())
            {
                item.Quality = 0;
                return;
            }

            var qualityModifier = -1;

            if (item.IsAgedBrie())
            {
                qualityModifier = 1;
            }
            
            item.ChangeQuality(qualityModifier);
        }

        public static void Update(this GildedRose.Item item)
        {
            if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                return;
            }

            var qualityModifier = -1;

            if (item.IsAgedBrie() || item.IsBackstagePass())
            {
                qualityModifier = 1;

                if (item.IsBackstagePass())
                {
                    if (item.SellIn < 6)
                    {
                        qualityModifier = 3;
                    }
                    else if (item.SellIn < 11)
                    {
                        qualityModifier = 2;
                    }
                }
            }
           
            item.ChangeQuality(qualityModifier);
            

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
                item.UpdateExpiredItem();
            }
        }
    }
}