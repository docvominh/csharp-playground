using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharp.Datetime;

public sealed class PartnerDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
{
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected a datetime string.");
        }

        var value = reader.GetString();

        const DateTimeStyles Styles =
            DateTimeStyles.AllowWhiteSpaces
            | DateTimeStyles.AssumeUniversal
            | DateTimeStyles.AdjustToUniversal;

        if (!DateTimeOffset.TryParse(
                value,
                CultureInfo.InvariantCulture,
                Styles,
                out var result
            ))
        {
            throw new JsonException($"Invalid datetime value: '{value}'.");
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();

            return;
        }

        writer.WriteStringValue(
            value.Value
                .ToUniversalTime()
                .ToString("O", CultureInfo.InvariantCulture)
        );
    }
}