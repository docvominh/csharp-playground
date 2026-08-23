using Microsoft.Extensions.Time.Testing;
using Shouldly;
using Xunit;

namespace CSharp.Datetime.WorkingWithDateTime;

public class BusinessClockTests
{
    [Fact]
    public void ReturnsVietnamTime_WhenServerLocationIsIrrelevant()
    {
        var fixedUtcNow = new DateTimeOffset(
            2026, 8, 21,
            8, 30, 0,
            TimeSpan.Zero);

        var fakeTime = new FakeTimeProvider(fixedUtcNow);
        var vietnamZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
        var clock = new BusinessClock(fakeTime, vietnamZone);
        var result = clock.GetLocalNow();

        result.Hour.ShouldBe(15);
        result.Offset.ShouldBe(TimeSpan.FromHours(7));
        result.ToUniversalTime().ShouldBe(fixedUtcNow);
    }

[Fact]
public void ClockCanBeAdvanced()
{
    var fakeTime = new FakeTimeProvider(
        new DateTimeOffset(
            2026, 1, 1,
            0, 0, 0,
            TimeSpan.Zero));

    fakeTime.Advance(TimeSpan.FromHours(2));

    fakeTime.GetUtcNow().ShouldBe( new DateTimeOffset(
        2026, 1, 1,
        2, 0, 0,
        TimeSpan.Zero));
}

[Fact]
public void NewYork_SkipsAnHour_WhenDaylightSavingStarts()
{
    var newYork = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");

    var beforeUtc = new DateTimeOffset(
        2025, 3, 9,
        6, 30, 0,
        TimeSpan.Zero);

    var afterUtc = new DateTimeOffset(
        2025, 3, 9,
        7, 30, 0,
        TimeSpan.Zero);

    var before = TimeZoneInfo.ConvertTime(beforeUtc, newYork);
    var after = TimeZoneInfo.ConvertTime(afterUtc, newYork);

    before.Hour.ShouldBe(1);
    before.Offset.ShouldBe(TimeSpan.FromHours(-5));

    after.Hour.ShouldBe(3);
    after.Offset.ShouldBe(TimeSpan.FromHours(-4));
}

[Fact]
public void NewYork_0230IsInvalid_WhenDaylightSavingStarts()
{
    var newYork = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");

    //On the second Sunday in March, America/New_York switches from EST (UTC-5) to EDT (UTC-4) at 2:00:00 AM local.
    //The clock doesn't tick through 2:00–2:59 — it jumps straight from 01:59:59 EST to 03:00:00 EDT.
    var localTime = new DateTime(
        2025, 3, 9,
        2, 30, 0,
        DateTimeKind.Unspecified);

    newYork.IsInvalidTime(localTime).ShouldBeTrue();
}

[Fact]
public void NewYork_0130IsAmbiguous_WhenDaylightSavingEnds()
{
    var newYork = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");

    // when DST ends in November, 01:00:00–01:59:59 happens twice (once in EDT, once in EST),
    // so that range is ambiguous instead of invalid — two possible UTC instants instead of zero.
    var localTime = new DateTime(
        2025, 11, 2,
        1, 30, 0,
        DateTimeKind.Unspecified);

    newYork.IsAmbiguousTime(localTime).ShouldBeTrue();
}
}