using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class
    InfusionSlotUpgradeSourceJsonConverter : JsonConverter<InfusionSlotUpgradeSource>
{
    public override InfusionSlotUpgradeSource? Read(
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
        InfusionSlotUpgradeSource value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static InfusionSlotUpgradeSource Read(JsonElement value)
    {
        return new InfusionSlotUpgradeSource
        {
            Upgrade = value.GetProperty("upgrade").GetEnum<InfusionSlotUpgradeKind>(),
            ItemId = value.GetProperty("item_id").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, InfusionSlotUpgradeSource value)
    {
        writer.WriteStartObject();
        writer.WriteString("upgrade", value.Upgrade.ToString());
        writer.WriteNumber("item_id", value.ItemId);
        writer.WriteEndObject();
    }
}
