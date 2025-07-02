namespace BankAccountWithMarten.EventSource.Events;

public class WithdrawEvent
{
    public required decimal Amount { get; set; }

    public override string ToString()
    {
        return $"Withdraw {Amount} from account";
    }
}