namespace CSharp.Collections;

public class SetCollection
{
    [Test]
    public void TestHashSet()
    {
        var books = new HashSet<Book>
        {
            new("7 Good Habit", 1998),
            new("Dummy C++", 1999),
            new("Dummy C++", 1999)
        };


        Assert.That(books.Count, Is.EqualTo(2));
    }

    [Test]
    public void TestSortedHashSet()
    {
        var books = new SortedSet<Book>(Comparer<Book>.Create((a, b) => a.Year.CompareTo(b.Year)))
        {
            new("7 Good Habit", 1998),
            new("Dummy C++", 1999),
            new("Dummy C++", 1999),
            new("Dummy C#", 1995)
        };

        Assert.That(books.Count, Is.EqualTo(3));
        Assert.That(books.ElementAt(0).Name, Is.EqualTo("Dummy C#"));
        Assert.That(books.ElementAt(1).Name, Is.EqualTo("7 Good Habit"));
        Assert.That(books.ElementAt(2).Name, Is.EqualTo("Dummy C++"));
    }
}