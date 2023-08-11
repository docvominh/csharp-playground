using System.Collections;

namespace CSharp.Collections;

public class ListCollection
{
    [Test]
    public void TestList()
    {
        var books = new List<Book>
        {
            new()
            {
                Name = "Atomic Habit",
                Year = 2005
            },
            new()
            {
                Name = "Team Geek",
                Year = 2012
            }
        };

        Assert.That(books.Count, Is.EqualTo(2));

        books.Add(new Book("7 Good Habit", 1998));
        books.Add(new Book("Dummy C++", 1999));

        Assert.That(books.Count, Is.EqualTo(4));

        var newBooks = books.FindAll(x => x.Year >= 2010).ToList();
        var oldBooks = from book in books
            where book.Year < 2010
            orderby book.Name
            select book;

        Assert.That(newBooks, Has.Count.EqualTo(1));
        Assert.That(oldBooks.Count, Is.EqualTo(3));
        Assert.That(oldBooks.ElementAt(0).Name, Is.EqualTo("7 Good Habit"));

        books.Sort((a, b) => b.Year.CompareTo(a.Year));
        Assert.That(books.ElementAt(0).Name, Is.EqualTo("Team Geek"));
    }
}