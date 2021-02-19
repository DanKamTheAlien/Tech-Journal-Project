using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    internal interface IJournalDatabase
    {
        void WriteEntryDatabase(string username, string entry);
        void ReadEntryDatabase();
    }
}
