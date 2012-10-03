using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
	[TestFixture]
	public class ItemAgingRules
	{
		[Test, UseReporter(typeof (DiffReporter))]
		public void PinDownTheZombie()
		{
			var testSubject = new GildedRose();
			var log = new StringBuilder();
			for (var i = 0; i < 50; i++)
			{
				this.Dump(testSubject, log, i);
				testSubject.UpdateQuality();
			}
			this.Dump(testSubject, log, 50);
			Approvals.Verify(log.ToString());
		}

		private void Dump(GildedRose testSubject, StringBuilder log, int iteration)
		{
			log.AppendFormat("After Iteration {0}", iteration);
			testSubject.DumpDebugInfo(log);
			log.AppendLine();
		}
	}
}