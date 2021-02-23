using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    internal interface ICrudService
    {
        void WriteEntry(AppInfo databaseInfo, string username, string entry);
        void ReadEntry(AppInfo databaseInfo);
        void DeleteEntry(AppInfo databaseInfo);
        void UpdateEntry(AppInfo databaseInfo, string entry);
    }
}
