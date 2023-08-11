namespace CSharp.OOP.Interface;

public class VipAccount : AccountImpl
{
    private const int DiscountPercent = 10;

    public VipAccount(string name, string number, double amount) : base(name, number, amount)
    {
    }

    public override double Purchase(double price)
    {
        Amount -= price - price / 100 * DiscountPercent;
        return Amount;
    }
}