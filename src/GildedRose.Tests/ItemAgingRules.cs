using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using FluentAssertions;

namespace ConsoleApp.Tests
{
	[TestFixture]
	public class ItemAgingRules
	{
		[Test, UseReporter(typeof(DiffReporter))]
		public void ShouldAlwaysPass()
		{
			var testSubject = new GildedRose();
			var log = new StringBuilder();
			for(int i = 0; i < 50; i++)
			{
				Dump(testSubject, log, i);
				testSubject.UpdateQuality();
			}
			Dump(testSubject, log, 50);
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
