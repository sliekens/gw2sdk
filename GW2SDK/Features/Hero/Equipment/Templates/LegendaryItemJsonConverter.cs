using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Templates;

internal sealed class LegendaryItemJsonConverter : JsonConverter<LegendaryItem>
{
    public override LegendaryItem? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static LegendaryItem Read(in JsonElement json)
    {
        return new LegendaryItem
        {
            Id = json.GetProperty("id").GetInt32(),
            MaxCount = json.GetProperty("max_count").GetInt32()
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        LegendaryItem value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static void Write(Utf8JsonWriter writer, LegendaryItem value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("max_count", value.MaxCount);
        writer.WriteEndObject();
    }
}
