namespace CSharp.Collections;

public class DictionaryCollection
{
    [Test]
    public void TestDictionary()
    {
        var books = new Dictionary<int, Book>
        {
            { 1, new Book("7 Good Habit", 2000) },
            { 3, new Book("Dummy C++", 1995) },
            { 2, new Book("Dummy Java", 1998) }
        };

        Assert.That(books.ElementAt(0).Key, Is.EqualTo(1));
        Assert.That(books.ElementAt(1).Key, Is.EqualTo(3));
        Assert.That(books.ElementAt(2).Key, Is.EqualTo(2));


        Assert.That(books[3].Name, Is.EqualTo("Dummy C++"));
    }

    [Test]
    public void SortedDictionary()
    {
        var books = new SortedDictionary<int, Book>
        {
            { 1, new Book("7 Good Habit", 2000) },
            { 3, new Book("Dummy C++", 1995) },
            { 2, new Book("Dummy Java", 1998) }
        };

        Assert.That(books.ElementAt(0).Key, Is.EqualTo(1));
        Assert.That(books.ElementAt(1).Key, Is.EqualTo(2));
        Assert.That(books.ElementAt(2).Key, Is.EqualTo(3));


        Assert.That(books[3].Name, Is.EqualTo("Dummy C++"));
    }
}