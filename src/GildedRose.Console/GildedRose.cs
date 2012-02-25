/*
 * Hi and welcome to team Gilded Rose. As you know, we are a small inn with a prime location in a prominent city
 * run by a friendly innkeeper named Allison. We also buy and sell only the finest goods. Unfortunately, our
 * goods are constantly degrading in quality as they approach their sell by date. We have a system in place that
 * updates our inventory for us. It was developed by a no-nonsense type named Leeroy, who has moved on to new
 * adventures. Your task is to add the new feature to our system so that we can begin selling a new category of
 * items. First an introduction to our system:

	- All items have a SellIn value which denotes the number of days we have to sell the item
	- All items have a Quality value which denotes how valuable the item is
	- At the end of each day our system lowers both values for every item

 * Pretty simple, right? Well this is where it gets interesting:

	- Once the sell by date has passed, Quality degrades twice as fast
	- The Quality of an item is never negative
	- "Aged Brie" actually increases in Quality the older it gets
	- The Quality of an item is never more than 50
	- "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
	- "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; Quality
		increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality
		drops to 0 after the concert

 * We have recently signed a supplier of conjured items. This requires an update to our system:

	- "Conjured" items degrade in Quality twice as fast as normal items. Conjured is an adjective; it can
		be applie to any item to make it conjured.

 * Feel free to make any changes to the UpdateQuality method and add any new code as long as everything
 * still works correctly. However, do not alter the Item class or Items property as those belong to the
 * goblin in the corner who will insta-rage and one-shot you as he doesn't believe in shared code ownership
 * (we have kindly placed this code into HereBeDragons.cs, so that you won't accidentally change it. This
 * also makes it easier for the goblin to tell when to insta-rage). Your work needs to be completed by
 * Friday, February 18, 2011 08:00:00 AM PST.

 * Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a
 * legendary item and as such its Quality is 80 and it never alters.

 * */

namespace GildedRose.Console
{
	public partial class GildedRose
	{
		private static void Main(string[] args)
		{
			System.Console.WriteLine("OMGHAI!");

			var app = new GildedRose();
			app.UpdateQuality();
			System.Console.ReadKey();
		}

		public void UpdateQuality()
		{
			for(int i = 0; i < _innventory.Count; i++)
			{
				if(_innventory[i].Name != "Aged Brie" && _innventory[i].Name != "Backstage passes to a TAFKAL80ETC concert")
				{
					if(_innventory[i].Quality > 0)
					{
						if(_innventory[i].Name != "Sulfuras, Hand of Ragnaros")
						{
							_innventory[i].Quality = _innventory[i].Quality - 1;
						}
					}
				}
				else
				{
					if(_innventory[i].Quality < 50)
					{
						_innventory[i].Quality = _innventory[i].Quality + 1;

						if(_innventory[i].Name == "Backstage passes to a TAFKAL80ETC concert")
						{
							if(_innventory[i].SellIn < 11)
							{
								if(_innventory[i].Quality < 50)
								{
									_innventory[i].Quality = _innventory[i].Quality + 1;
								}
							}

							if(_innventory[i].SellIn < 6)
							{
								if(_innventory[i].Quality < 50)
								{
									_innventory[i].Quality = _innventory[i].Quality + 1;
								}
							}
						}
					}
				}

				if(_innventory[i].Name != "Sulfuras, Hand of Ragnaros")
				{
					_innventory[i].SellIn = _innventory[i].SellIn - 1;
				}

				if(_innventory[i].SellIn < 0)
				{
					if(_innventory[i].Name != "Aged Brie")
					{
						if(_innventory[i].Name != "Backstage passes to a TAFKAL80ETC concert")
						{
							if(_innventory[i].Quality > 0)
							{
								if(_innventory[i].Name != "Sulfuras, Hand of Ragnaros")
								{
									_innventory[i].Quality = _innventory[i].Quality - 1;
								}
							}
						}
						else
						{
							_innventory[i].Quality = _innventory[i].Quality - _innventory[i].Quality;
						}
					}
					else
					{
						if(_innventory[i].Quality < 50)
						{
							_innventory[i].Quality = _innventory[i].Quality + 1;
						}
					}
				}
			}
		}
	}
}
