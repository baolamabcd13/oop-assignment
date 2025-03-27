using Entities;
using System.Collections.Generic;
using Npgsql;

namespace DataAccess
{
    public class BookDAL
    {
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new NpgsqlCommand("SELECT * FROM \"tblBook\"", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book()
                    {
                        BookCode = reader["BookCode"].ToString(),
                        BookName = reader["BookName"].ToString(),
                        PublisherCode = reader["PublisherCode"].ToString()
                    });
                }
            }
            return books;
        }

        public void AddBook(Book book)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    "INSERT INTO \"tblBook\" (\"BookCode\", \"BookName\", \"PublisherCode\") VALUES (@code, @name, @pubCode)", conn);
                cmd.Parameters.AddWithValue("@code", book.BookCode);
                cmd.Parameters.AddWithValue("@name", book.BookName);
                cmd.Parameters.AddWithValue("@pubCode", book.PublisherCode);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Book> SearchBooks(string keyword)
        {
            List<Book> books = new List<Book>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    "SELECT * FROM \"tblBook\" WHERE \"BookName\" ILIKE @kw OR \"BookCode\" ILIKE @kw", conn);
                cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book()
                    {
                        BookCode = reader["BookCode"].ToString(),
                        BookName = reader["BookName"].ToString(),
                        PublisherCode = reader["PublisherCode"].ToString()
                    });
                }
            }
            return books;
        }
    }
}
