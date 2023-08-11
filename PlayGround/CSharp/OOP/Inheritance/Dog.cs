namespace CSharp.OOP.Inheritance;

public class Dog : Animal
{
    public override void Shout()
    {
        Console.WriteLine("Bark Bark");
    }

    public override void Eat()
    {
        base.Eat();
        Console.WriteLine("Meat");
    }
}