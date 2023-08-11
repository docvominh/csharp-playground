namespace CSharp.Collections;

public class Book
{
    public string Name { get; set; }
    public int Year { get; set; }


    public Book()
    {
    }

    public Book(string name, int year)
    {
        Name = name;
        Year = year;
    }

    private bool Equals(Book other)
    {
        return Name == other.Name && Year == other.Year;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Book)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Year);
    }
}