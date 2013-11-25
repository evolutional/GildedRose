using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp.Tests
{
    [TestClass]
    public class BuildVerificationTests
    {
        [TestMethod, UseReporter(typeof (DiffReporter))]
        public void AgeOurStandardInventoryForSeveralMonthsAndVerifyEachDaysResults()
        {
            var testSubject = new GildedRose();
            var log = new StringBuilder();
            for (int i = 0; i < 50; i++)
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