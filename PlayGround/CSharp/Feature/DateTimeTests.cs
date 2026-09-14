using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CSharp.Feature;

public class DateTimeTests
{
    private static readonly CultureInfo VietnamCulture = CultureInfo.GetCultureInfo("vi-VN");

    [Fact]
    public void DefaultDateTime_IsMinValue()
    {
        var minValue = new DateTime();

        minValue.ShouldBe(DateTime.MinValue);
    }

    [Fact]
    public void VietnameseCulture_ShortDatePattern_IsDayMonthYear()
    {
        VietnamCulture.DateTimeFormat.ShortDatePattern.ShouldBe("dd/MM/yyyy");
    }

    [Fact]
    public void Constructor_WithComponents_SetsExpectedProperties()
    {
        // parameters order year, month, day, hour, minute, second
        var dateFromNumber = new DateTime(2023, 12, 20, 9, 30, 0);

        dateFromNumber.Year.ShouldBe(2023);
        dateFromNumber.Month.ShouldBe(12);
        dateFromNumber.Day.ShouldBe(20);
        dateFromNumber.Hour.ShouldBe(9);
        dateFromNumber.Minute.ShouldBe(30);
        dateFromNumber.Second.ShouldBe(0);
    }

    [Fact]
    public void Now_UtcNow_Today_HaveExpectedKindsAndValues()
    {
        var now = DateTime.Now;
        var utcNow = DateTime.UtcNow;
        var today = DateTime.Today;

        now.Kind.ShouldBe(DateTimeKind.Local);
        utcNow.Kind.ShouldBe(DateTimeKind.Utc);
        today.TimeOfDay.ShouldBe(TimeSpan.Zero);
        today.Date.ShouldBe(now.Date);
    }

    [Fact]
    public void Parse_WithVietnameseCulture_ParsesDayMonthYearFormat()
    {
        var dateString = "25/12/2023 18:30:25";

        var dateValue = DateTime.Parse(dateString, VietnamCulture);

        dateValue.ShouldBe(new DateTime(2023, 12, 25, 18, 30, 25));
    }

    [Fact]
    public void ParseExact_WithInvariantCulture_ParsesDayMonthYearFormat()
    {
        var dateString = "25/12/2023 18:30:25";

        var dateValue = DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        dateValue.ShouldBe(new DateTime(2023, 12, 25, 18, 30, 25));
    }

    [Fact]
    public void ConvertToDateTime_WithVietnameseCulture_ParsesDayMonthYearFormat()
    {
        var dateString = "25/12/2023 18:30:25";

        var dateValue = Convert.ToDateTime(dateString, VietnamCulture);

        dateValue.ShouldBe(new DateTime(2023, 12, 25, 18, 30, 25));
    }

    [Fact]
    public void ToString_WithInvariantCulture_FormatsAsMonthDayYear()
    {
        var dateFromNumber = new DateTime(2023, 12, 20, 9, 30, 0);

        dateFromNumber.ToString(CultureInfo.InvariantCulture).ShouldBe("12/20/2023 09:30:00");
    }

    [Fact]
    public void ToString_WithGermanCulture_FormatsWithDotsAndDayMonthYear()
    {
        var dateFromNumber = new DateTime(2023, 12, 20, 9, 30, 0);

        dateFromNumber.ToString(CultureInfo.GetCultureInfo("de-DE")).ShouldBe("20.12.2023 09:30:00");
    }

    [Fact]
    public void ToString_WithCustomFormat_FormatsAsDayMonthYear()
    {
        var dateFromNumber = new DateTime(2023, 12, 20, 9, 30, 0);

        dateFromNumber.ToString("dd-MM-yyyy HH:mm:ss").ShouldBe("20-12-2023 09:30:00");
    }

    [Fact]
    public void StringInterpolation_WithCustomFormat_FormatsAsDayMonthYear()
    {
        var dateFromNumber = new DateTime(2023, 12, 20, 9, 30, 0);

        $"{dateFromNumber:dd-MM-yyyy HH:mm:ss}".ShouldBe("20-12-2023 09:30:00");
    }

    [Fact]
    public void AddDays_ShiftsDateAsExpected()
    {
        var today = new DateTime(2024, 1, 1, 9, 30, 0);

        var yesterday = today.AddDays(-1);
        var next30Days = today.AddDays(30);

        yesterday.ShouldBe(new DateTime(2023, 12, 31, 9, 30, 0));
        next30Days.ShouldBe(new DateTime(2024, 1, 31, 9, 30, 0));
    }

    [Fact]
    public void AddMethods_ShiftDateTimeComponentsAsExpected()
    {
        var today = new DateTime(2024, 1, 1, 9, 30, 0);

        today.AddSeconds(1).ShouldBe(new DateTime(2024, 1, 1, 9, 30, 1));
        today.AddMinutes(1).ShouldBe(new DateTime(2024, 1, 1, 9, 31, 0));
        today.AddHours(1).ShouldBe(new DateTime(2024, 1, 1, 10, 30, 0));
        today.AddDays(1).ShouldBe(new DateTime(2024, 1, 2, 9, 30, 0));
        today.AddMonths(1).ShouldBe(new DateTime(2024, 2, 1, 9, 30, 0));
        today.AddYears(1).ShouldBe(new DateTime(2025, 1, 1, 9, 30, 0));
    }

    [Fact]
    public void Serialize_WithDefaultOptions_UsesIso8601Format()
    {
        var today = new DateTime(2024, 1, 1, 9, 30, 0);

        // System.Text.Json.JsonSerializer uses ISO-8601 time format by default https://en.wikipedia.org/wiki/ISO_8601
        JsonSerializer.Serialize(today).ShouldBe("\"2024-01-01T09:30:00\"");
    }

    [Fact]
    public void Serialize_WithCustomConverter_UsesCustomFormat()
    {
        var today = new DateTime(2024, 1, 1, 9, 30, 0);
        var serializerOptions = new JsonSerializerOptions
        {
            Converters = { new SimpleDateTimeConvert("dd-MM-yyyy HH:mm:ss") }
        };

        JsonSerializer.Serialize(today, serializerOptions).ShouldBe("\"01-01-2024 09:30:00\"");
    }

    [Fact]
    public void Deserialize_WithCustomConverter_ParsesCustomFormat()
    {
        var serializerOptions = new JsonSerializerOptions
        {
            Converters = { new SimpleDateTimeConvert("dd-MM-yyyy HH:mm:ss") }
        };

        var orderJson =
            """
            {
                "Id" : 1,
                "PurchaseDate" : "01-01-2024 09:30:00"
            }
            """;

        var order = JsonSerializer.Deserialize<Order>(orderJson, serializerOptions);

        order.ShouldNotBeNull();
        order!.PurchaseDate.ShouldBe(new DateTime(2024, 1, 1, 9, 30, 0));
    }
}

internal class SimpleDateTimeConvert : JsonConverter<DateTime>
{
    private readonly string _format;

    public SimpleDateTimeConvert(string format)
    {
        _format = format;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString()!, _format, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format, CultureInfo.InvariantCulture));
    }
}

internal class Order
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
}
