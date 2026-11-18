using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Upgrades;

internal sealed class GuildUpgradeCoinsCostJsonConverter : JsonConverter<GuildUpgradeCoinsCost>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_upgrade_coins_cost";

    public override GuildUpgradeCoinsCost Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildUpgradeCoinsCost value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildUpgradeCoinsCost Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement coinsJson = json.GetProperty("coins");
        return new GuildUpgradeCoinsCost
        {
            Coins = new GuildWars2.Coin(coinsJson.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildUpgradeCoinsCost value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("coins", value.Coins.Amount);
        writer.WriteEndObject();
    }
}
