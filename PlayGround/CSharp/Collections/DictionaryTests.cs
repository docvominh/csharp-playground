namespace CSharp.Collections;

public class DictionaryTests
{
    [Fact]
    public void Dictionary_SimpleAccess()
    {
        Dictionary<int, Book> books = new()
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
    public void Dictionary_AdvancedAccess()
    {
        Dictionary<int, Book> books = new()
        {
            { 1, new Book("7 Good Habit", 2000) },
            { 3, new Book("Dummy C++", 1995) },
            { 2, new Book("Dummy Java", 1998) }
        };

        books.TryGetValue(2, out Book? dummyJava);
        dummyJava.ShouldNotBeNull();
        dummyJava.Name.ShouldBe("Dummy Java");

        books.TryGetValue(99, out Book? dummyAi);
        dummyAi.ShouldBeNull();

        Init(out int value);
        value.ShouldBe(5);

        var y = 10;
        Modify(ref y);
        y.ShouldBe(11);
    }

    [Fact]
    public void SortedDictionary()
    {
        SortedDictionary<int, Book> sortedBooks = new()
        {
            { 1, new Book("7 Good Habit", 2000) },
            { 3, new Book("Dummy C++", 1995) },
            { 2, new Book("Dummy Java", 1998) }
        };

        sortedBooks.ElementAt(0).Key.ShouldBe(1);
        sortedBooks.ElementAt(1).Key.ShouldBe(2);
        sortedBooks.ElementAt(2).Key.ShouldBe(3);

        sortedBooks[3].Name.ShouldBe("Dummy C++");
    }

    private static void Init(out int value)
    {
        value = 5;
    }

    private static void Modify(ref int value)
    {
        value++;
    }

    private static void Normal(int value)
    {
        value++;
    }
}
