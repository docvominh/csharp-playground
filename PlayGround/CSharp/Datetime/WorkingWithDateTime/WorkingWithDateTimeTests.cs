using Shouldly;
using Xunit;

namespace CSharp.Datetime.WorkingWithDateTime;

public class WorkingWithDateTimeTests
{
    [Fact]
    public void ReturnsVietnamLocalHour()
    {
        var utcTime = new DateTimeOffset(
            2026, 8, 21,
            8, 30, 0,
            TimeSpan.Zero);

        var localTime = utcTime.ToLocalTime();

        localTime.Hour.ShouldBe(15);
    }

[Fact]
public void ReturnsVietnamLocalHourWithCorrectZone()
{
    var utcTime = new DateTimeOffset(
        2026, 8, 21,
        8, 30, 0,
        TimeSpan.Zero);

    var vietnamZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
    var vietnamTime = TimeZoneInfo.ConvertTime(utcTime, vietnamZone);

    vietnamTime.Hour.ShouldBe(15);
    vietnamTime.Offset.ShouldBe(TimeSpan.FromHours(7));
}
}