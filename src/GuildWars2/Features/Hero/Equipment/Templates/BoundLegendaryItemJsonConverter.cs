using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Templates;

internal sealed class BoundLegendaryItemJsonConverter : JsonConverter<BoundLegendaryItem>
{
    public override BoundLegendaryItem? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static BoundLegendaryItem Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Count = json.GetProperty("count").GetInt32()
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        BoundLegendaryItem value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("count", value.Count);
        writer.WriteEndObject();
    }
}
