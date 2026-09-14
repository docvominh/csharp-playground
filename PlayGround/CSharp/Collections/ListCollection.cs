using System.Collections;

namespace CSharp.Collections;

public class ListCollection
{
    [Fact]
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

        books.Count.ShouldBe(2);

        books.Add(new Book("7 Good Habit", 1998));
        books.Add(new Book("Dummy C++", 1999));

        books.Count.ShouldBe(4);

        var newBooks = books.FindAll(x => x.Year >= 2010).ToList();
        var oldBooks = from book in books
            where book.Year < 2010
            orderby book.Name
            select book;

        newBooks.Count.ShouldBe(1);
        oldBooks.Count().ShouldBe(3);
        oldBooks.ElementAt(0).Name.ShouldBe("7 Good Habit");

        books.Sort((a, b) => b.Year.CompareTo(a.Year));
        books.ElementAt(0).Name.ShouldBe("Team Geek");
    }
}