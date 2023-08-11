namespace CSharp.OOP.Inheritance;

public class Test
{
    [Test]
    public void Test1()
    {
        var dog = new Dog { Name = "Dog" };
        var cat = new Cat { Name = "Cat" };
        dog.Shout();
        dog.Eat();

        cat.Shout();
        cat.Eat();
    }
}