using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal sealed class GuildUpgradeCollectibleCostJsonConverter : JsonConverter<GuildUpgradeCollectibleCost>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_upgrade_collectible_cost";

    public override GuildUpgradeCollectibleCost Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildUpgradeCollectibleCost value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildUpgradeCollectibleCost Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement nameJson = json.GetProperty("name");
        JsonElement itemIdJson = json.GetProperty("item_id");
        JsonElement countJson = json.GetProperty("count");
        return new GuildUpgradeCollectibleCost
        {
            Name = nameJson.GetStringRequired(),
            ItemId = itemIdJson.GetInt32(),
            Count = countJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildUpgradeCollectibleCost value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteString("name", value.Name);
        writer.WriteNumber("item_id", value.ItemId);
        writer.WriteNumber("count", value.Count);
        writer.WriteEndObject();
    }
}
