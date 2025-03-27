using Npgsql;

namespace DataAccess
{
    public class DatabaseHelper
    {
        private static string connectionString = "Host=localhost;Port=5432;Database=LibraryDB;Username=postgres;Password=Jay582003";

        public static NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(connectionString);
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            return conn;
        }
    }
}
