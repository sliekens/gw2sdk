using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildUpgradeCollectibleCostJson
{
    public static GuildUpgradeCollectibleCost GetGuildUpgradeCollectibleCost(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember count = "count";
        RequiredMember itemId = "item_id";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Collectible"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == count.Name)
            {
                count = member;
            }
            else if (member.Name == itemId.Name)
            {
                itemId = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildUpgradeCollectibleCost
        {
            Name = name.Map(value => value.GetString()) ?? "",
            ItemId = itemId.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32())
        };
    }
}
