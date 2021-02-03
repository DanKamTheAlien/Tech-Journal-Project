using System;
using System.Collections.Generic;

namespace Tech_Journal_ConsoleApp
{
    public class Journal: IJournal
    {
        public List<string> JournalEntries { get; }

        public Journal()
        {
            JournalEntries = new List<string>();
        }

        public void CreateJournalEntry(string entry)
        {
            JournalEntries.Add(entry);
        }

        public string GetEntryInput()
        {
            Console.WriteLine("Please enter today's journal entry:");
            var entry = Console.ReadLine();
            return entry;
        }
    }
}
