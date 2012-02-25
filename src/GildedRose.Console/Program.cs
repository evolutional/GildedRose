namespace GildedRose.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			System.Console.WriteLine("OMGHAI!");

			var app = new GildedRose();
			app.UpdateQuality();
			System.Console.ReadKey();
		}
	}
}