namespace CSharp.EveryThingPassByValue;

public class Test
{
    [Test]
    public void TestReference()
    {
        var text = "Hello";
        var newText = ChangeText(text);
        Assert.That(text, Is.Not.EqualTo(newText));


        // Person is reference type
        var person = new Person
        {
            Name = "Minh",
            Age = 33
        };

        var newPerson = ChangePerson(person);

        Assert.That(person.GetHashCode(), Is.EqualTo(newPerson.GetHashCode()));
        Assert.That(person, Is.EqualTo(newPerson));

        // Struct is value type
        var car = new Car()
        {
            Name = "Lexus",
            Age = 2
        };

        var newCar = ChangeCar(car);
        Assert.That(car, Is.Not.EqualTo(newCar));

        newCar = ChangeCar(ref car);
        Assert.That(car, Is.EqualTo(newCar));

        Car emptyCar;
        SetUpCar(out emptyCar);

        Assert.That(emptyCar.Name, Is.EqualTo("Vios"));
    }

    private String ChangeText(String text)
    {
        text += " World";
        return text;
    }

    private Person ChangePerson(Person person)
    {
        person.Age += 10;
        return person;
    }

    private Car ChangeCar(Car car)
    {
        car.Age += 10;
        return car;
    }

    private Car ChangeCar(ref Car car)
    {
        car.Age += 10;
        return car;
    }

    private void SetUpCar(out Car car)
    {
        car = new Car()
        {
            Name = "Vios",
            Age = 1
        };
    }
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

struct Car
{
    public string Name { get; set; }
    public int Age { get; set; }
}