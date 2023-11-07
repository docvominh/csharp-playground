using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunction;

public static class MyFirstFunction
{
    [Function("MyFirstFunction")]
    public static void Run([TimerTrigger("0 */1 * * * *")] MyInfo myTimer, FunctionContext context)
    {
        HttpClient client = new HttpClient();
        var logger = context.GetLogger("MyFirstFunction");
        logger.LogInformation("C# Timer trigger function executed at: {Now}", DateTime.Now);
        logger.LogInformation("Next timer schedule at: {ScheduleStatusNext}", myTimer.ScheduleStatus.Next);
    }
}

public class MyInfo
{
    public MyScheduleStatus ScheduleStatus { get; set; }

    public bool IsPastDue { get; set; }
}

public class MyScheduleStatus
{
    public DateTime Last { get; set; }

    public DateTime Next { get; set; }

    public DateTime LastUpdated { get; set; }
}