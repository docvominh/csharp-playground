using System.Globalization;

namespace CSharp.Feature;

public class DateTimeOffsetTests
{
    [Fact]
    public void DefaultDateTimeOffset_IsMinValue()
    {
        DateTimeOffset today = new DateTimeOffset();

        today.ShouldBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public void Now_MatchesLocalTimeZoneOffset()
    {
        var now = DateTimeOffset.Now;

        now.Offset.ShouldBe(TimeZoneInfo.Local.GetUtcOffset(now.DateTime));
    }

    [Fact]
    public void UtcNow_HasZeroOffset()
    {
        DateTimeOffset.UtcNow.Offset.ShouldBe(TimeSpan.Zero);
    }

    [Fact]
    public void DateTimeUtcNow_HasUtcKind()
    {
        DateTime.UtcNow.Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Fact]
    public void SpecifyKind_ChangesKindWithoutConvertingValue()
    {
        var unspecified = new DateTime(2024, 6, 15, 9, 0, 0, DateTimeKind.Unspecified);

        var asUtc = DateTime.SpecifyKind(unspecified, DateTimeKind.Utc);

        asUtc.Kind.ShouldBe(DateTimeKind.Utc);
        asUtc.ShouldBe(new DateTime(2024, 6, 15, 9, 0, 0, DateTimeKind.Utc));
    }

    [Fact]
    public void Constructor_WithOffset_SetsExpectedProperties()
    {
        var todayx = new DateTimeOffset(2024, 1, 1, 11, 30, 0, new TimeSpan(7, 0, 0));

        todayx.DateTime.ShouldBe(new DateTime(2024, 1, 1, 11, 30, 0));
        todayx.Offset.ShouldBe(TimeSpan.FromHours(7));
        todayx.ToUniversalTime().ShouldBe(new DateTimeOffset(2024, 1, 1, 4, 30, 0, TimeSpan.Zero));
    }

    [Fact]
    public void ToString_WithCustomFormat_IncludesOffset()
    {
        var todayx = new DateTimeOffset(2024, 1, 1, 11, 30, 0, new TimeSpan(7, 0, 0));

        todayx.ToString("dd/MM/yyyy HH:mm:ss zzz").ShouldBe("01/01/2024 11:30:00 +07:00");
    }

    [Fact]
    public void ParseExact_WithCustomFormat_ParsesOffsetCorrectly()
    {
        var utcTimeString = "2024-01-01T11:30:00+07:00";

        var utcTime = DateTimeOffset.ParseExact(utcTimeString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);

        utcTime.DateTime.ShouldBe(new DateTime(2024, 1, 1, 11, 30, 0));
        utcTime.Offset.ShouldBe(TimeSpan.FromHours(7));
        utcTime.ToUniversalTime().ShouldBe(new DateTimeOffset(2024, 1, 1, 4, 30, 0, TimeSpan.Zero));
    }

    [Fact]
    public void ToString_AfterParsingWithOffset_FormatsAsReadablePattern()
    {
        var utcTimeString = "2024-01-01T11:30:00+07:00";
        var utcTime = DateTimeOffset.ParseExact(utcTimeString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);

        // Format ISO-8601 to a more readable pattern
        utcTime.ToString("dd-MM-yyyy HH:mm:ss zzz").ShouldBe("01-01-2024 11:30:00 +07:00");
    }
}
