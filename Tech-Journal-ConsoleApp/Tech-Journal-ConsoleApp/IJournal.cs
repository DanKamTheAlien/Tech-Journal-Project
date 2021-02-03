using System;
using System.Collections.Generic;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    interface IJournal
    {
        List<string> JournalEntries { get; }
        void CreateJournalEntry(string entry);
        string GetEntryInput();
    }
}
