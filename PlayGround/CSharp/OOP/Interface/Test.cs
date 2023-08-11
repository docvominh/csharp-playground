namespace CSharp.OOP.Interface;

public class Test
{
    [Test]
    public void TestInterface()
    {
        IAccount account = new Account("PHAM DUC MINH", "99995555", 1000.5);
        account.Purchase(500);
        Assert.That(account.GetAmount(), Is.EqualTo(500.5));

        IAccount vipAccount = new VipAccount(account.GetName(), account.GetNumber(), account.GetAmount());
        vipAccount.Purchase(100);
        Assert.That(vipAccount.GetAmount(), Is.EqualTo(410.5));
    }
}