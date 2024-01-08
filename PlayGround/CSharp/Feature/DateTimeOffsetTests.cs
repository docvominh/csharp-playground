using System.Globalization;
using FluentAssertions;

namespace CSharp.Feature;

public class DateTimeOffsetTests
{
    [Test]
    public void Test()
    {
        DateTimeOffset today = new DateTimeOffset();
        Console.WriteLine(today); // 01/01/0001 00:00:00 +00:00

        today.Should().Be(DateTimeOffset.MinValue); // True

        today = DateTimeOffset.Now;
        Console.WriteLine(today.ToString());

        today = DateTimeOffset.UtcNow;
        Console.WriteLine(today);

        var test =  DateTime.UtcNow;
        Console.WriteLine(test);

        var utcNow =  DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        Console.WriteLine(today);
        Console.WriteLine(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern);

        var todayx = new DateTimeOffset(2024, 1, 1, 11, 30, 00, new TimeSpan(7, 0, 0));
        Console.WriteLine(todayx); // 01/01/2024 11:30:00 +07:00

        Console.WriteLine(DateTimeOffset.Now);     // Local machine Vietnam UTC+7 time  08/01/2024 11:40:38 +07:00
        Console.WriteLine(DateTimeOffset.UtcNow); // Basis of utc time (UTC or UTC+0)   08/01/2024 04:40:38 +00:00

        Console.WriteLine(DateTimeOffset.UtcNow.ToString("dd-MM-yyyy HH:ss:mmzzz"));

        var utcTimeString = "2024-01-01T11:30:00+07:00";
        var utcTime = DateTimeOffset.ParseExact(utcTimeString, "yyyy-MM-ddTHH:ss:mmzzz", CultureInfo.InvariantCulture);
        // Format ISO-8061 to better readable pattern
        Console.WriteLine(utcTime.ToString("dd-MM-yyyy HH:ss:mm zzz")); // 01-01-2024 11:30:00 +07:00
    }
}
