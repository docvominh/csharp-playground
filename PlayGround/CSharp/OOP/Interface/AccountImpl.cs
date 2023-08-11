namespace CSharp.OOP.Interface;

public abstract class AccountImpl : IAccount
{
    protected string Name { get; }
    protected string Number { get; }
    protected double Amount { get; set; }

    protected AccountImpl(string name, string number, double amount)
    {
        Name = name;
        Number = number;
        Amount = amount;
    }

    public string GetName()
    {
        return Name;
    }

    public string GetNumber()
    {
        return Number;
    }

    public double GetAmount()
    {
        return Amount;
    }

    public abstract double Purchase(double price);
}