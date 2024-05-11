using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfusionSlotUpgradePathJson
{
    public static InfusionSlotUpgradePath GetInfusionSlotUpgradePath(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InfusionSlotUpgradePath
        {
            Upgrade = upgrade.Map(static value => value.GetEnum<InfusionSlotUpgradeKind>()),
            ItemId = itemId.Map(static value => value.GetInt32())
        };
    }
}
