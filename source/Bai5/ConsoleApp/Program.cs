using System;
using System.Collections.Generic;
using System.IO;
using Entities;
using DataAccess;

namespace ConsoleApp
{
    class Program
    {
        static void ExportBooksToCsv(List<Book> books, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("BookCode,BookName,PublisherCode");
                foreach (var b in books)
                {
                    writer.WriteLine($"{b.BookCode},{b.BookName},{b.PublisherCode}");
                }
            }
        }

        static void Main(string[] args)
        {
            var bookDAL = new BookDAL();
            var pubDAL = new PublisherDAL();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== QUẢN LÝ THƯ VIỆN (PostgreSQL) ===");
                Console.WriteLine("1. Hiển thị danh sách sách");
                Console.WriteLine("2. Thêm sách");
                Console.WriteLine("3. Hiển thị danh sách nhà xuất bản");
                Console.WriteLine("4. Thêm nhà xuất bản");
                Console.WriteLine("5. Tìm kiếm sách theo tên hoặc mã");
                Console.WriteLine("6. Thống kê số lượng sách theo NXB");
                Console.WriteLine("7. Xuất danh sách sách ra file CSV");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var books = bookDAL.GetAllBooks();
                        foreach (var b in books)
                            Console.WriteLine($"{b.BookCode} - {b.BookName} - {b.PublisherCode}");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Write("Mã sách: ");
                        var code = Console.ReadLine();
                        Console.Write("Tên sách: ");
                        var name = Console.ReadLine();
                        Console.Write("Mã NXB: ");
                        var pubCode = Console.ReadLine();
                        bookDAL.AddBook(new Book { BookCode = code, BookName = name, PublisherCode = pubCode });
                        Console.WriteLine("Thêm sách thành công!");
                        Console.ReadKey();
                        break;

                    case "3":
                        var pubs = pubDAL.GetAllPublishers();
                        foreach (var p in pubs)
                            Console.WriteLine($"{p.PublisherCode} - {p.PublisherName} - {p.Phone}");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Write("Mã NXB: ");
                        var pc = Console.ReadLine();
                        Console.Write("Tên NXB: ");
                        var pn = Console.ReadLine();
                        Console.Write("Địa chỉ: ");
                        var addr = Console.ReadLine();
                        Console.Write("SĐT: ");
                        var phone = Console.ReadLine();
                        pubDAL.AddPublisher(new Publisher { PublisherCode = pc, PublisherName = pn, Address = addr, Phone = phone });
                        Console.WriteLine("Thêm NXB thành công!");
                        Console.ReadKey();
                        break;

                    case "5":
                        Console.Write("Nhập từ khóa cần tìm: ");
                        var keyword = Console.ReadLine();
                        var results = bookDAL.SearchBooks(keyword);
                        foreach (var b in results)
                            Console.WriteLine($"{b.BookCode} - {b.BookName} - {b.PublisherCode}");
                        Console.ReadKey();
                        break;

                    case "6":
                        var stats = pubDAL.GetPublisherWithBooks();
                        foreach (var entry in stats)
                        {
                            var pub = entry.Key;
                            var booksByPub = entry.Value;
                            Console.WriteLine($"\n{pub.PublisherCode} - {pub.PublisherName} | Số sách: {booksByPub.Count}");
                            foreach (var b in booksByPub)
                                Console.WriteLine($"\t{b.BookCode} - {b.BookName}");
                        }
                        Console.ReadKey();
                        break;

                    case "7":
                        var allBooks = bookDAL.GetAllBooks();
                        Console.Write("Nhập tên file (ví dụ: books.csv): ");
                        var file = Console.ReadLine();
                        ExportBooksToCsv(allBooks, file);
                        Console.WriteLine($"Đã xuất danh sách sách ra file {file}!");
                        Console.ReadKey();
                        break;

                    case "0": return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}