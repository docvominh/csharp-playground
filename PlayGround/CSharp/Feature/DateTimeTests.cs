using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CSharp.Feature;

public class DateTimeTests
{
    [Test]
    public void InitializeDateTime()
    {
        var minValue = new DateTime();
        Console.WriteLine(minValue); // 01/01/0001 00:00:00
        Console.WriteLine(minValue.Equals(DateTime.MinValue)); // True

        // Check for min value datetime
        minValue.Should().Be(DateTime.MinValue);

        // Get current system default datetime format
        var sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        Console.WriteLine(sysFormat); // dd/MM/yyyy on my machine

        // parameters order year, month,… day, hour, minute, second
        var dateFromNumber = new DateTime(2023, 12, 20, 9, 30, 0);
        Console.WriteLine(dateFromNumber); // 20/12/2023 09:30:00

        // Return current date and time on this computer
        DateTime date1 = DateTime.Now;
        DateTime date2 = DateTime.UtcNow;
        DateTime date3 = DateTime.Today;

        Console.WriteLine(date1); // 04/01/2024 11:47:28
        Console.WriteLine(date2); // 04/01/2024 04:47:28
        Console.WriteLine(date3); // 04/01/2024 00:00:00


        var dateString = "25/12/2023 18:30:25";

        // Parse use default CultureInfo.CurrentCulture
        var dateValue = DateTime.Parse(dateString);

        // Specific Culture
        dateValue = DateTime.Parse(dateString, CultureInfo.CurrentCulture);

        // Use format pattern
        dateValue = DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        // User convert class
        dateValue = Convert.ToDateTime(dateString);

        Console.WriteLine(dateValue); // 25/12/2023 18:30:25

        // Use different Culture to format datetime
        Console.WriteLine(dateFromNumber.ToString(CultureInfo.InvariantCulture)); // 12/20/2023 09:30:00
        Console.WriteLine(dateFromNumber.ToString(CultureInfo.GetCultureInfo("en-US"))); // 12/20/2023 9:30:00AM
        Console.WriteLine(dateFromNumber.ToString(CultureInfo.GetCultureInfo("de-DE"))); // 20.12.2023 09:30:00

        // Custom format
        Console.WriteLine(dateFromNumber.ToString("dd-MM-yyyy HH:mm:ss")); // 20-12-2023 09:30:00

        // C# 6 String interpolation format
        Console.WriteLine($"{dateFromNumber:dd-MM-yyyy HH:mm:ss}"); // 20-12-2023 09:30:00

        var today = new DateTime(2024, 1, 1, 9, 30, 0);
        var yesterday = today.AddDays(-1);
        var next30Days = today.AddDays(30);

        // Result: 31/12/2023 09:30:00 - 01/01/2024 09:30:00 - 31/01/2024 09:30:00
        Console.WriteLine($"{yesterday} - {today} - {next30Days}");

        // Another Add function
        today.AddSeconds(1);
        today.AddMinutes(1);
        today.AddHours(1);
        today.AddDays(1);
        today.AddMonths(1);
        today.AddYears(1);

        // System.Text.Json.JsonSerializer user default ISO-8601 time format https://en.wikipedia.org/wiki/ISO_8601
        // ex: "2022-01-31T13:15:05.2151663-05:00"
        Console.WriteLine(JsonSerializer.Serialize(today)); //"2024-01-01T09:30:00"

        var serializerOptions = new JsonSerializerOptions
        {
            Converters = { new SimpleDateTimeConvert("dd-MM-yyyy HH:mm:ss") }
        };

        Console.WriteLine(JsonSerializer.Serialize(today, serializerOptions)); //"01-01-2024 09:30:00"

        var orderJson =
            """
            {
                "order" : {
                    "Id" : 1,
                    "PurchaseDate" : "01-01-2024 09:30:00"
                }
            }
            """;

        var order = JsonSerializer.Deserialize<Order>(orderJson, serializerOptions);
    }
}

class SimpleDateTimeConvert : JsonConverter<DateTime>
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

class Order
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
}
