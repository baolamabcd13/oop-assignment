using System;
using System.Collections.Generic;

public class BookList
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void DisplayBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Publisher: {book.Publisher}, Year: {book.Year}, ISBN: {book.ISBN}");
        }
    }

    public void SortBooks(IComparer<Book> comparer = null)
    {
        if (comparer == null)
        {
            books.Sort((b1, b2) => b1.Title.CompareTo(b2.Title));
        }
        else
        {
            books.Sort(comparer);
        }
    }

    public void DisplayStatistics()
{
    var authorCount = new Dictionary<string, int>();
    var publisherCount = new Dictionary<string, int>();
    var yearCount = new Dictionary<int, int>();

    foreach (var book in books)
    {
        if (authorCount.ContainsKey(book.Author))
            authorCount[book.Author]++;
        else
            authorCount[book.Author] = 1;

        if (publisherCount.ContainsKey(book.Publisher))
            publisherCount[book.Publisher]++;
        else
            publisherCount[book.Publisher] = 1;

        if (yearCount.ContainsKey(book.Year))
            yearCount[book.Year]++;
        else
            yearCount[book.Year] = 1;
    }

    Console.WriteLine("\nThống kê số lượng sách theo tác giả:");
    foreach (var author in authorCount)
        Console.WriteLine($"{author.Key}: {author.Value}");

    Console.WriteLine("\nThống kê số lượng sách theo nhà xuất bản:");
    foreach (var publisher in publisherCount)
        Console.WriteLine($"{publisher.Key}: {publisher.Value}");

    Console.WriteLine("\nThống kê số lượng sách theo năm xuất bản:");
        foreach (var year in yearCount)
            Console.WriteLine($"{year.Key}: {year.Value}");
    }
}