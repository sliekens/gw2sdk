using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Treasury;

internal static class GuildTreasurySlotJson
{
    public static GuildTreasurySlot GetGuildTreasurySlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember itemId = "item_id";
        RequiredMember count = "count";
        RequiredMember countNeededForUpgrade = "needed_by";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(itemId.Name))
            {
                itemId = member;
            }
            else if (member.NameEquals(count.Name))
            {
                count = member;
            }
            else if (member.NameEquals(countNeededForUpgrade.Name))
            {
                countNeededForUpgrade = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildTreasurySlot
        {
            ItemId = itemId.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32()),
            CountNeededForUpgrades = countNeededForUpgrade.Map(
                values => values.GetList(
                    value => value.GetCountNeededForUpgrade(missingMemberBehavior)
                )
            )
        };
    }
}
