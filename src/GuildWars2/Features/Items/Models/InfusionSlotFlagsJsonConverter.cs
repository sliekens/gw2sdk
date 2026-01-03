using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class InfusionSlotFlagsJsonConverter : JsonConverter<InfusionSlotFlags>
{
    public override InfusionSlotFlags? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        InfusionSlotFlags value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static InfusionSlotFlags Read(in JsonElement json)
    {
        return new()
        {
            Enrichment = json.GetProperty("enrichment").GetBoolean(),
            Infusion = json.GetProperty("infusion").GetBoolean(),
            Other = json.GetProperty("other").GetList(static (in value) => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, InfusionSlotFlags value)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("enrichment", value.Enrichment);
        writer.WriteBoolean("infusion", value.Infusion);
        writer.WriteStartArray("other");
        foreach (string other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
