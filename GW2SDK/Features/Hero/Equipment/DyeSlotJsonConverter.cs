using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal sealed class DyeSlotJsonConverter : JsonConverter<DyeSlot>
{
    public override DyeSlot? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, DyeSlot value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static DyeSlot? Read(in JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return new DyeSlot
        {
            ColorId = json.GetProperty("color_id").GetInt32(),
            Material = json.GetProperty("material").GetEnum<Material>()
        };
    }

    public static void Write(Utf8JsonWriter writer, DyeSlot value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("color_id", value.ColorId);
        writer.WriteString("material", value.Material.ToString());
        writer.WriteEndObject();
    }
}
