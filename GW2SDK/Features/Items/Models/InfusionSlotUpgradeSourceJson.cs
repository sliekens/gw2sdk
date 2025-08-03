using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfusionSlotUpgradeSourceJson
{
    public static InfusionSlotUpgradeSource GetInfusionSlotUpgradeSource(this in JsonElement json)
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

        return new InfusionSlotUpgradeSource
        {
            Upgrade = upgrade.Map(static (in JsonElement value) => value.GetEnum<InfusionSlotUpgradeKind>()),
            ItemId = itemId.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
