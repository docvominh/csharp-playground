namespace CSharp.Feature;

public class Nullable
{
    // Promise with Compiler that _privateText won't be null
    private string _privateText = null!;

    [Fact]
    public void TestNullable()
    {
        _privateText = string.Empty;
        _privateText.ShouldNotBeNull();

        // <Nullable>enable</Nullable> warning all the null var/property
        string text2 = null;
        text2.ShouldBeNull();

        string? text3 = null;
        text3.ShouldBeNull();


        var person = GetPerson();
        (person.Address?.HouseNumber).ShouldBeNull();
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