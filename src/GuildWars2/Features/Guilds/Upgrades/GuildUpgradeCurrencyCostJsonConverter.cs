using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal sealed class GuildUpgradeCurrencyCostJsonConverter : JsonConverter<GuildUpgradeCurrencyCost>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_upgrade_currency_cost";

    public override GuildUpgradeCurrencyCost Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildUpgradeCurrencyCost value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildUpgradeCurrencyCost Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement nameJson = json.GetProperty("name");
        JsonElement countJson = json.GetProperty("count");
        return new GuildUpgradeCurrencyCost
        {
            Name = nameJson.GetStringRequired(),
            Count = countJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildUpgradeCurrencyCost value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteString("name", value.Name);
        writer.WriteNumber("count", value.Count);
        writer.WriteEndObject();
    }
}
