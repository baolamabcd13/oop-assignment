public class Book : IBook
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }
    public string ISBN { get; set; }
    public List<string> Chapters { get; set; }

    public Book(string title, string author, string publisher, int year, string isbn, List<string> chapters)
    {
        Title = title;
        Author = author;
        Publisher = publisher;
        Year = year;
        ISBN = isbn;
        Chapters = chapters;
    }
}