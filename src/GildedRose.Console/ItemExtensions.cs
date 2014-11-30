using System.Net.NetworkInformation;

namespace ConsoleApp
{
    public static class ItemExtensions
    {
        private static void ChangeQuality(this GildedRose.Item item, int modifier)
        {
            if (item.IsConjured())
            {
                modifier *= 2;
            }

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


        private static bool IsConjured(this GildedRose.Item item)
        {
            return item.Name.StartsWith("Conjured ");
        }

        private static string BaseName(this GildedRose.Item item)
        {
            var name = item.Name;
            if (item.IsConjured())
            {
                name = name.Remove(0, 9);
            }
            return name;
        }

        private static bool IsAgedBrie(this GildedRose.Item item)
        {
            return item.BaseName() == "Aged Brie";
        }

        private static bool IsBackstagePass(this GildedRose.Item item)
        {
            return item.BaseName() == "Backstage passes to a TAFKAL80ETC concert";
        }

        private static bool IsSulfuras(this GildedRose.Item item)
        {
            return item.BaseName() == "Sulfuras, Hand of Ragnaros";
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
            if (item.IsSulfuras())
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