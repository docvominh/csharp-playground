using CSharp.Collections;

namespace CSharp.OOP.Generic;

public class Test
{
    [Test]
    public void TestGeneric()
    {
        BookService service = new BookService();

        var book = new Book("The important thing", 1995);

        var saved = service.Save(book);

        Assert.That(book, Is.EqualTo(saved));
    }
}