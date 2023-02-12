using System.Data.SqlClient;
using System;
using System.Data;

namespace notepad_app.Helper
{
    public class DBHelper
    {
        public static Tuple<DataTable, string> ConnectDB()
        {
            var _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = _configuration.Build();
            
            string connectionString = config.GetConnectionString("NotepadConnectionString");
            string queryString = "SELECT * FROM notepad";

            DataTable notepadTable = new DataTable();
            SqlConnection conn;
            using (conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(queryString, conn);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        notepadTable.Load(reader);
                    }

                    conn.Close();
                    return new Tuple<DataTable, string>(notepadTable, "");
                }
                catch (Exception ex) 
                {          
                    return new Tuple<DataTable, string>(notepadTable, $"Error Connecting to database: {ex.Message}");
                }
            }
        }

        public static void CreateNewNote(string title, string notes)
        {
            var _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = _configuration.Build();

            string connectionString = config.GetConnectionString("NotepadConnectionString");
            string queryString = $"INSERT INTO notepad VALUES ('{title}', '{notes}', '{DateTime.Now}');";

            DataTable notepadTable = new DataTable();
            SqlConnection conn;
            using (conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(queryString, conn);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    
                }
                catch (Exception ex)
                {
                   
                }
            }
        }

    }
}
