using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Upgrades;

internal sealed class GuildUpgradeCostJsonConverter : JsonConverter<GuildUpgradeCost>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_upgrade_cost";

    public override GuildUpgradeCost Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildUpgradeCost value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildUpgradeCost Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case GuildUpgradeCoinsCostJsonConverter.DiscriminatorValue:
                    return GuildUpgradeCoinsCostJsonConverter.Read(json);
                case GuildUpgradeCollectibleCostJsonConverter.DiscriminatorValue:
                    return GuildUpgradeCollectibleCostJsonConverter.Read(json);
                case GuildUpgradeCurrencyCostJsonConverter.DiscriminatorValue:
                    return GuildUpgradeCurrencyCostJsonConverter.Read(json);
                case GuildUpgradeItemCostJsonConverter.DiscriminatorValue:
                    return GuildUpgradeItemCostJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        return new GuildUpgradeCost();
    }

    public static void Write(Utf8JsonWriter writer, GuildUpgradeCost value)
    {
        if (value is GuildUpgradeCoinsCost guildUpgradeCoinsCost)
        {
            GuildUpgradeCoinsCostJsonConverter.Write(writer, guildUpgradeCoinsCost);
        }
        else if (value is GuildUpgradeCollectibleCost guildUpgradeCollectibleCost)
        {
            GuildUpgradeCollectibleCostJsonConverter.Write(writer, guildUpgradeCollectibleCost);
        }
        else if (value is GuildUpgradeCurrencyCost guildUpgradeCurrencyCost)
        {
            GuildUpgradeCurrencyCostJsonConverter.Write(writer, guildUpgradeCurrencyCost);
        }
        else if (value is GuildUpgradeItemCost guildUpgradeItemCost)
        {
            GuildUpgradeItemCostJsonConverter.Write(writer, guildUpgradeItemCost);
        }
        else if (value.GetType() == typeof(GuildUpgradeCost))
        {
            writer.WriteStartObject();
            writer.WriteString(DiscriminatorName, DiscriminatorValue);
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
