using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace Tech_Journal_ConsoleApp
{
    class JournalDatabase:IJournalDatabase
    {
        private readonly string _dataSource = @"DESKTOP-72V5PVK";
        readonly string _database = "Journal"; 
        readonly string _username = "test"; 
        readonly string _password = "test"; 
        private SqlConnection EstablishConnection()
        {
            
            string connString = @"Data Source=" + _dataSource + ";Initial Catalog="
                        + _database + ";Persist Security Info=True;User ID=" + _username + ";Password=" + _password;
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

        public void WriteEntryDatabase(string username, string entry)
        {
            var conn = EstablishConnection();
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO Journal_details (Date, Name, Entry) VALUES ");
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

        public void ReadEntryDatabase()
        {
            var conn = EstablishConnection();
            string sqlQuery = "SELECT Date, Name, Entry FROM Journal_details";
            var output = "";
            SqlCommand command = new SqlCommand(sqlQuery, conn);
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                output = output + data.GetValue(0) + " - " + data.GetValue(1) + " - " + data.GetValue(2) + "\n";
            }
            Console.WriteLine(output);
        }
    }
}

