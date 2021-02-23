using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    internal interface ICrudService
    {
        void WriteEntry(string username, string entry);
        void ReadEntry();
        void DeleteEntry();
        void UpdateEntry(string entry);
    }
}
