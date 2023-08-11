namespace CSharp.OOP.Interface;

public class Account : AccountImpl
{
    public Account(string name, string number, double amount) : base(name, number, amount)
    {
    }

    public override double Purchase(double price)
    {
        Amount -= price;
        return Amount;
    }
}