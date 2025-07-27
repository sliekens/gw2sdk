using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class InfusionSlotJsonConverter : JsonConverter<InfusionSlot>
{
    public override InfusionSlot? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        InfusionSlot value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static InfusionSlot Read(in JsonElement json)
    {
        return new InfusionSlot
        {
            Flags = InfusionSlotFlagsJsonConverter.Read(json.GetProperty("flags")),
            ItemId = json.GetProperty("item_id").GetNullableInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, InfusionSlot value)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("flags");
        InfusionSlotFlagsJsonConverter.Write(writer, value.Flags);
        if (value.ItemId.HasValue)
        {
            writer.WriteNumber("item_id", value.ItemId.Value);
        }
        else
        {
            writer.WriteNull("item_id");
        }

        writer.WriteEndObject();
    }
}
