namespace CSharp.Collections;

public class SetCollection
{
    [Fact]
    public void TestHashSet()
    {
        var books = new HashSet<Book>
        {
            new("7 Good Habit", 1998),
            new("Dummy C++", 1999),
            new("Dummy C++", 1999)
        };


        books.Count.ShouldBe(2);
    }

    [Fact]
    public void TestSortedHashSet()
    {
        var books = new SortedSet<Book>(Comparer<Book>.Create((a, b) => a.Year.CompareTo(b.Year)))
        {
            new("7 Good Habit", 1998),
            new("Dummy C++", 1999),
            new("Dummy C++", 1999),
            new("Dummy C#", 1995)
        };

        books.Count.ShouldBe(3);
        books.ElementAt(0).Name.ShouldBe("Dummy C#");
        books.ElementAt(1).Name.ShouldBe("7 Good Habit");
        books.ElementAt(2).Name.ShouldBe("Dummy C++");
    }
}