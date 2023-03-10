using System.Data.SqlClient;
using System;
using System.Data;

namespace notepad_app.Helper
{
    public static class DBHelper
    {
        public static string? ReturnConnectionString()
        {
            string? connectionString = string.Empty;


                var _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                var config = _configuration.Build();
                connectionString = config.GetConnectionString("NotepadConnectionString");


            return connectionString;
        }
        public static Tuple<DataTable, string> ConnectDB(string connectionString)
        {
            //string? connectionString = DBHelper.ReturnConnectionString();

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

        public static void CreateNewNote(string title, string notes, string connectionString)
        {
            //string? connectionString = DBHelper.ReturnConnectionString();
            string queryString = $"INSERT INTO notepad VALUES ('{title}', '{notes}', '{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")}');";

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
