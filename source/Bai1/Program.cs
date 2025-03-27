using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        BookList bookList = new BookList();

        Console.Write("Nhập số lượng sách: ");
        int bookCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < bookCount; i++)
        {
            Console.WriteLine($"\nNhập thông tin cho cuốn sách thứ {i + 1}:");

            Console.Write("Tên sách: ");
            string title = Console.ReadLine();

            Console.Write("Tên tác giả: ");
            string author = Console.ReadLine();

            Console.Write("Nhà xuất bản: ");
            string publisher = Console.ReadLine();

            Console.Write("Năm xuất bản: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();

            Console.Write("Số lượng chương: ");
            int chapterCount = int.Parse(Console.ReadLine());

            List<string> chapters = new List<string>();
            for (int j = 0; j < chapterCount; j++)
            {
                Console.Write($"Tên chương {j + 1}: ");
                chapters.Add(Console.ReadLine());
            }

            bookList.AddBook(new Book(title, author, publisher, year, isbn, chapters));
        }

        Console.WriteLine("\nDanh sách sách:");
        bookList.DisplayBooks();

        bookList.SortBooks();
        Console.WriteLine("\nDanh sách sách sau khi sắp xếp:");
        bookList.DisplayBooks();

        bookList.DisplayStatistics();
    }
}