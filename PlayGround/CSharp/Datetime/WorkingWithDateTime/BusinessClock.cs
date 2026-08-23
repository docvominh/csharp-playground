namespace CSharp.Datetime.WorkingWithDateTime;

public sealed class BusinessClock(TimeProvider timeProvider, TimeZoneInfo timeZone)
{
    public DateTimeOffset GetUtcNow()
    {
        return timeProvider.GetUtcNow();
    }

    public DateTimeOffset GetLocalNow()
    {
        return TimeZoneInfo.ConvertTime(
            timeProvider.GetUtcNow(),
            timeZone);
    }
}