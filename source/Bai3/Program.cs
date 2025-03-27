using System;
using System.Collections.Generic;
using Npgsql;

class Program
{
    static string connectionString = "Host=localhost;Username=postgres;Password=Jay582003;Database=ProductCatalog";

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add a Product");
            Console.WriteLine("2. List Products");
            Console.WriteLine("3. Find a Product");
            Console.WriteLine("4. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    ListProducts();
                    break;
                case 3:
                    FindProduct();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void AddProduct()
    {
        Console.Write("Enter the product's name to add: ");
        string name = Console.ReadLine();

        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("INSERT INTO products (name) VALUES (@name) ON CONFLICT (name) DO NOTHING", conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"The {name} has been added successfully!");
                }
                else
                {
                    Console.WriteLine($"The {name} already exists.");
                }
            }
        }
    }

    static void ListProducts()
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT name FROM products", conn))
            using (var reader = cmd.ExecuteReader())
            {
                Console.WriteLine("Products:");
                while (reader.Read())
                {
                    Console.WriteLine($"- {reader.GetString(0)}");
                }
            }
        }
    }

    static void FindProduct()
    {
        Console.Write("Enter the product's name to find: ");
        string name = Console.ReadLine();

        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM products WHERE name = @name", conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                long count = (long)cmd.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine($"Product {name} found.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
        }
    }
}