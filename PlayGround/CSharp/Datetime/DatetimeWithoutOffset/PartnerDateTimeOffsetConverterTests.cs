using System.Text.Json;
using Shouldly;
using Xunit;

namespace CSharp.Datetime.DatetimeWithoutOffset;

public class PartnerDateTimeOffsetConverterTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters =
        {
            new PartnerDateTimeOffsetConverter()
        }
    };

    [Fact]
    public void LegacyValueWithoutOffset_IsAssumedToBeUtc()
    {
        var result = JsonSerializer.Deserialize<DateTimeOffset?>("\"2026-01-15T10:30:00\"", _options);

        result.ShouldBe( new DateTimeOffset(
            2026, 1, 15,
            10, 30, 0,
            TimeSpan.Zero));
    }

    [Fact]
    public void NewValueWithUtcMarker_IsSupported()
    {
        var result = JsonSerializer.Deserialize<DateTimeOffset?>("\"2026-01-15T10:30:00Z\"", _options);

        result?.Offset.ShouldBe(TimeSpan.Zero);
        result?.Hour.ShouldBe(10);
    }

    [Fact]
    public void ValueWithOffset_IsConvertedToUtc()
    {
        var result = JsonSerializer.Deserialize<DateTimeOffset?>("\"2026-01-15T10:30:00+01:00\"", _options);

        result?.Offset.ShouldBe(TimeSpan.Zero);
        result?.Hour.ShouldBe(9);
    }
}