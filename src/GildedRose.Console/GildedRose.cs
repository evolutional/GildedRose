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
