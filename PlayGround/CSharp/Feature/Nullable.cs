namespace CSharp.Feature;

public class Nullable
{
    // Promise with Compiler that _privateText won't be null
    private string _privateText = null!;

    [Test]
    public void TestNullable()
    {
        _privateText = string.Empty;
        Assert.That(_privateText, Is.Not.Null);

        // <Nullable>enable</Nullable> warning all the null var/property
        string text2 = null;
        Assert.That(text2, Is.Null);
        
        string? text3 = null;
        Assert.That(text3, Is.Null);


        var person = GetPerson();
        Assert.That(person.Address?.HouseNumber, Is.Null);
    }

    private Person GetPerson()
    {
        return new Person();
    }
}

class Person
{
    public string Name { get; set; }
    public Address Address { get; set; }
}

class Address
{
    public int HouseNumber { get; set; }
}