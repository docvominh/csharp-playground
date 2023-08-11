namespace CSharp.OOP.Interface;

public interface IAccount
{
    string GetName();
    string GetNumber();
    double GetAmount();
    double Purchase(double price);
}