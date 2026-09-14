namespace CSharp.EveryThingPassByValue;

public class Test
{
    [Fact]
    public void TestReference()
    {
        var text = "Hello";
        var newText = ChangeText(text);
        text.ShouldNotBe(newText);


        // Person is reference type
        var person = new Person
        {
            Name = "Minh",
            Age = 33
        };

        var newPerson = ChangePerson(person);

        person.GetHashCode().ShouldBe(newPerson.GetHashCode());
        person.ShouldBe(newPerson);

        // Struct is value type
        var car = new Car()
        {
            Name = "Lexus",
            Age = 2
        };

        var newCar = ChangeCar(car);
        car.ShouldNotBe(newCar);

        newCar = ChangeCar(ref car);
        car.ShouldBe(newCar);

        Car emptyCar;
        SetUpCar(out emptyCar);

        emptyCar.Name.ShouldBe("Vios");
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