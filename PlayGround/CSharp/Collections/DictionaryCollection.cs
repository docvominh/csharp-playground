namespace CSharp.Collections;

public class DictionaryCollection
{
    [Fact]
    public void TestDictionary()
    {
        var books = new Dictionary<int, Book>
        {
            { 1, new Book("7 Good Habit", 2000) },
            { 3, new Book("Dummy C++", 1995) },
            { 2, new Book("Dummy Java", 1998) }
        };

        books.ElementAt(0).Key.ShouldBe(1);
        books.ElementAt(1).Key.ShouldBe(3);
        books.ElementAt(2).Key.ShouldBe(2);


        books[3].Name.ShouldBe("Dummy C++");
    }

    [Fact]
    public void SortedDictionary()
    {
        var books = new SortedDictionary<int, Book>
        {
            { 1, new Book("7 Good Habit", 2000) },
            { 3, new Book("Dummy C++", 1995) },
            { 2, new Book("Dummy Java", 1998) }
        };

        books.ElementAt(0).Key.ShouldBe(1);
        books.ElementAt(1).Key.ShouldBe(2);
        books.ElementAt(2).Key.ShouldBe(3);


        books[3].Name.ShouldBe("Dummy C++");
    }
}