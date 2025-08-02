using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildUpgradeCostJson
{
    public static GuildUpgradeCost GetGuildUpgradeCost(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Coins":
                    return json.GetGuildUpgradeCoinsCost();
                case "Collectible":
                    return json.GetGuildUpgradeCollectibleCost();
                case "Currency":
                    return json.GetGuildUpgradeCurrencyCost();
                case "Item":
                    return json.GetGuildUpgradeItemCost();
            }
        }

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
                }
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildUpgradeCost();
    }
}
