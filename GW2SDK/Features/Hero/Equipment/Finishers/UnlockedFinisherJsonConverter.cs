using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Finishers;

internal sealed class UnlockedFinisherJsonConverter : JsonConverter<UnlockedFinisher>
{
    public override UnlockedFinisher? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var document = JsonDocument.ParseValue(ref reader);
        return Read(document.RootElement);
    }

    public static UnlockedFinisher? Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Permanent = json.GetProperty("permanent").GetBoolean(),
            Quantity = json.GetProperty("quantity").GetNullableInt32()
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnlockedFinisher value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteBoolean("permanent", value.Permanent);
        writer.WritePropertyName("quantity");
        if (value.Quantity is not null)
        {
            writer.WriteNumberValue(value.Quantity.Value);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WriteEndObject();
    }
}
