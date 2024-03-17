using System.Text.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildUpgradeCostJson
{
    public static GuildUpgradeCost GetGuildUpgradeCost(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Coins":
                return json.GetGuildUpgradeCoinsCost(missingMemberBehavior);
            case "Collectible":
                return json.GetGuildUpgradeCollectibleCost(missingMemberBehavior);
            case "Currency":
                return json.GetGuildUpgradeCurrencyCost(missingMemberBehavior);
            case "Item":
                return json.GetGuildUpgradeItemCost(missingMemberBehavior);
        }

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildUpgradeCost();
    }
}
