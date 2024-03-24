using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfusionSlotUpgradeSourceJson
{
    public static InfusionSlotUpgradeSource GetInfusionSlotUpgradeSource(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember upgrade = "upgrade";
        RequiredMember itemId = "item_id";
        foreach (var member in json.EnumerateObject())
        {
            if (upgrade.Match(member))
            {
                upgrade = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InfusionSlotUpgradeSource
        {
            Upgrade = upgrade.Map(
                value => value.GetEnum<InfusionSlotUpgradeKind>(missingMemberBehavior)
            ),
            ItemId = itemId.Map(value => value.GetInt32())
        };
    }
}
