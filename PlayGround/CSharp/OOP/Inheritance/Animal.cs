namespace CSharp.OOP.Inheritance;

public abstract class Animal
{
    public string? Name { get; set; }

    public virtual void Eat()
    {
        Console.Write($"{Name} eat...");
    }

    public abstract void Shout();
}