namespace CSharp.OOP.Interface;

public class Test
{
    [Fact]
    public void TestInterface()
    {
        IAccount account = new Account("PHAM DUC MINH", "99995555", 1000.5);
        account.Purchase(500);
        account.GetAmount().ShouldBe(500.5);

        IAccount vipAccount = new VipAccount(account.GetName(), account.GetNumber(), account.GetAmount());
        vipAccount.Purchase(100);
        vipAccount.GetAmount().ShouldBe(410.5);
    }
}