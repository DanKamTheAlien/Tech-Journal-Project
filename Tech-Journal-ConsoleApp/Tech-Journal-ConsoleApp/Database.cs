using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace Tech_Journal_ConsoleApp
{
    class Database
    {
        public SqlConnection EstablishConnection()
        {
            var datasource = @"DESKTOP-72V5PVK";//your server
            var database = "Journal"; //your database name
            var username = "test"; //username of server to connect
            var password = "test"; //password
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            var conn = new SqlConnection(connString);


            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                conn.Open();

                Console.WriteLine("Connection successful!");
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return null;
        }

        public void WriteEntry(string entry)
        {
            var conn = EstablishConnection();
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO Journal_details (Date, Entry) VALUES ");
            strBuilder.Append($"(N'{DateTime.Now:yyyy-MM-dd}', N'{entry}') ");

            string sqlQuery = strBuilder.ToString();
            using (SqlCommand command = new SqlCommand(sqlQuery, conn)) //pass SQL query created above and connection
            {
                command.ExecuteNonQuery(); //execute the Query
                Console.WriteLine("Query Executed.");
            }
        }
    }
}

