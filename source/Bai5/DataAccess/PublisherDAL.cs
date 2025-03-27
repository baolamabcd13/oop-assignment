using Entities;
using System.Collections.Generic;
using Npgsql;

namespace DataAccess
{
    public class PublisherDAL
    {
        public List<Publisher> GetAllPublishers()
        {
            List<Publisher> pubs = new List<Publisher>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new NpgsqlCommand("SELECT * FROM \"tblPublisher\"", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pubs.Add(new Publisher()
                    {
                        PublisherCode = reader["PublisherCode"].ToString(),
                        PublisherName = reader["PublisherName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Phone = reader["Phone"].ToString()
                    });
                }
            }
            return pubs;
        }

        public void AddPublisher(Publisher pub)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    "INSERT INTO \"tblPublisher\" (\"PublisherCode\", \"PublisherName\", \"Address\", \"Phone\") VALUES (@code, @name, @addr, @phone)", conn);
                cmd.Parameters.AddWithValue("@code", pub.PublisherCode);
                cmd.Parameters.AddWithValue("@name", pub.PublisherName);
                cmd.Parameters.AddWithValue("@addr", pub.Address);
                cmd.Parameters.AddWithValue("@phone", pub.Phone);
                cmd.ExecuteNonQuery();
            }
        }

        public Dictionary<Publisher, List<Book>> GetPublisherWithBooks()
        {
            Dictionary<Publisher, List<Book>> data = new Dictionary<Publisher, List<Book>>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    "SELECT p.\"PublisherCode\", p.\"PublisherName\", p.\"Address\", p.\"Phone\", " +
                    "b.\"BookCode\", b.\"BookName\" FROM \"tblPublisher\" p LEFT JOIN \"tblBook\" b ON p.\"PublisherCode\" = b.\"PublisherCode\" ORDER BY p.\"PublisherCode\"", conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var pub = new Publisher()
                    {
                        PublisherCode = reader["PublisherCode"].ToString(),
                        PublisherName = reader["PublisherName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Phone = reader["Phone"].ToString()
                    };

                    if (!data.ContainsKey(pub))
                        data[pub] = new List<Book>();

                    if (reader["BookCode"] != DBNull.Value)
                    {
                        data[pub].Add(new Book()
                        {
                            BookCode = reader["BookCode"].ToString(),
                            BookName = reader["BookName"].ToString(),
                            PublisherCode = pub.PublisherCode
                        });
                    }
                }
            }
            return data;
        }
    }
}
