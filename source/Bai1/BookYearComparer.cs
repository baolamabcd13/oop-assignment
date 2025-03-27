using System.Collections.Generic;

public class BookYearComparer : IComparer<Book>
{
    public int Compare(Book x, Book y)
    {
        return x.Year.CompareTo(y.Year);
    }
}