using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildUpgradeCollectibleCostJson
{
    public static GuildUpgradeCollectibleCost GetGuildUpgradeCollectibleCost(this JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember count = "count";
        RequiredMember itemId = "item_id";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Collectible"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildUpgradeCollectibleCost
        {
            Name = name.Map(static value => value.GetString()) ?? "",
            ItemId = itemId.Map(static value => value.GetInt32()),
            Count = count.Map(static value => value.GetInt32())
        };
    }
}
