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

        public void WriteEntry(string username, string entry)
        {
            var conn = EstablishConnection();
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

        public void ReadEntry()
        {
            var conn = EstablishConnection();
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

        public void UpdateEntry(string entry)
        {
            var conn = EstablishConnection();
            string sqlQuery = "UPDATE Journal SET Entry='"+$"{entry}"+"' WHERE id in (select top 1 id from Journal order by id desc )";
            SqlCommand command = new SqlCommand(sqlQuery, conn);
            conn.Close();
            conn.Dispose();
        }

        public void DeleteEntry()
        {
            var conn = EstablishConnection();
            string sqlQuery = "DELETE FROM Journal where id in (select top 1 id from Journal order by id desc )";
            SqlCommand command = new SqlCommand(sqlQuery, conn);
            conn.Close();
            conn.Dispose();
        }
    }
}

