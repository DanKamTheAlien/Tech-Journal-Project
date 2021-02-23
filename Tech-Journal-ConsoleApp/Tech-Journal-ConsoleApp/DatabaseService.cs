using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace Tech_Journal_ConsoleApp
{
    class DatabaseService:ICrudService
    {
        private SqlConnection EstablishConnection(AppInfo databaseInfo)
        {
            
            string connString = @"Data Source=" + databaseInfo.Datasource + ";Initial Catalog="
                        + databaseInfo.Database + ";Persist Security Info=True;User ID=" + databaseInfo.DatabaseUser + ";Password=" + databaseInfo.DatabasePassword;
            var connection= new SqlConnection(connString);

            try
            {
                Console.WriteLine("Opening Connection ...");
                connection.Open();
                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return connection;
        }


        public void WriteEntry(AppInfo databaseInfo, string username, string entry)
        {
            var conn = EstablishConnection(databaseInfo);
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO Journal (Date, Name, Entry) VALUES ");
            strBuilder.Append($"(N'{DateTime.Now:yyyy-MM-dd}', N'{username}', N'{entry}') ");
            string sqlQuery = strBuilder.ToString();
            using (SqlCommand command = new SqlCommand(sqlQuery, conn)) //pass SQL query created above and connection
            {
                command.ExecuteNonQuery(); //execute the Query
                Console.WriteLine("Query Executed.");
            }
            conn.Close();
            conn.Dispose();
        }

        public void ReadEntry(AppInfo databaseInfo)
        {
            var conn = EstablishConnection(databaseInfo);
            string sqlQuery = "SELECT Date, Name, Entry FROM Journal";
            var output = "";
            SqlCommand command = new SqlCommand(sqlQuery, conn);
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                output = output + data.GetValue(0) + " - " + data.GetValue(1) + " - " + data.GetValue(2) + "\n";
            }
            Console.WriteLine(output);
            conn.Close();
            conn.Dispose();
        }

        public void UpdateEntry(AppInfo databaseInfo, string entry)
        {
            var conn = EstablishConnection(databaseInfo);
            string sqlQuery = "UPDATE Journal SET Entry='" + $"{entry}" + "' WHERE id in (select top 1 id from Journal order by id desc )";
            SqlCommand command = new SqlCommand(sqlQuery, conn);
            SqlDataReader data = command.ExecuteReader();
            conn.Close();
            conn.Dispose();
        }

        public void DeleteEntry(AppInfo databaseInfo)
        {
            var conn = EstablishConnection(databaseInfo);
            string sqlQuery = "DELETE FROM Journal where id in (select top 1 id from Journal order by id desc )";
            SqlCommand command = new SqlCommand(sqlQuery, conn);
            SqlDataReader data = command.ExecuteReader();
            conn.Close();
            conn.Dispose();
        }
    }
}

