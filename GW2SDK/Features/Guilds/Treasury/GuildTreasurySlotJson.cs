using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Treasury;

internal static class GuildTreasurySlotJson
{
    public static GuildTreasurySlot GetGuildTreasurySlot(
        this JsonElement json
    )
    {
        RequiredMember itemId = "item_id";
        RequiredMember count = "count";
        RequiredMember countNeededForUpgrade = "needed_by";

        foreach (var member in json.EnumerateObject())
        {
            if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (countNeededForUpgrade.Match(member))
            {
                countNeededForUpgrade = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildTreasurySlot
        {
            ItemId = itemId.Map(static value => value.GetInt32()),
            Count = count.Map(static value => value.GetInt32()),
            CountNeededForUpgrades = countNeededForUpgrade.Map(static values => values.GetList(static value => value.GetCountNeededForUpgrade()
                )
            )
        };
    }
}
