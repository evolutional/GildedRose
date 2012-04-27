using System;

namespace ConsoleApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("OMGHAI!");

			var app = new GildedRose();
			app.UpdateQuality();
			Console.ReadKey();
		}
	}
}