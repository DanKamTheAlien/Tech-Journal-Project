using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Tech_Journal_ConsoleApp;
using System.Net.Mail;

namespace Tech_Journal.Test
{
    [TestFixture]
    public class JournalEntryTests
    {
        [Test]
        public void JournalEntries_ShouldReturnCorrectAmountOfJournalEntries()
        {
            var sut = new Journal();
            var sut1 = new List<string> { "test" };
            sut.CreateJournalEntry("Testing");
            Assert.AreEqual(sut.JournalEntries.Count,1);
        }

        [Test]
        public void JournalEntries_ShouldContainTheUsersInput()
        {
            var sut = new Journal();
            var sut1 = new List<string> {"test"};
            sut.CreateJournalEntry("Testing");
            Assert.AreEqual(sut.JournalEntries[0], "Testing");
        }
    }
}
