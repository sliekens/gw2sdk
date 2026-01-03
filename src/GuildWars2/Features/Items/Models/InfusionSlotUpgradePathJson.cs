using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfusionSlotUpgradePathJson
{
    public static InfusionSlotUpgradePath GetInfusionSlotUpgradePath(this in JsonElement json)
    {
        RequiredMember upgrade = "upgrade";
        RequiredMember itemId = "item_id";
        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new InfusionSlotUpgradePath
        {
            Upgrade = upgrade.Map(static (in value) => value.GetEnum<InfusionSlotUpgradeKind>()),
            ItemId = itemId.Map(static (in value) => value.GetInt32())
        };
    }
}
