using System;
using System.Collections.Generic;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    interface IEntry
    {
        List<string> JournalEntries { get; }
        void CreateJournalEntry(string entry);
        string GetEntryInput();
    }
}
