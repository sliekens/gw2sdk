using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Treasury;

[PublicAPI]
public static class GuildTreasurySlotJson
{
    public static GuildTreasurySlot GetGuildTreasurySlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember itemId = new("item_id");
        RequiredMember count = new("count");
        RequiredMember countNeededForUpgrade = new("needed_by");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (member.NameEquals(countNeededForUpgrade.Name))
            {
                countNeededForUpgrade.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildTreasurySlot
        {
            ItemId = itemId.Select(value => value.GetInt32()),
            Count = count.Select(value => value.GetInt32()),
            CountNeededForUpgrades = countNeededForUpgrade.SelectMany(
                value => value.GetCountNeededForUpgrade(missingMemberBehavior)
            )
        };
    }
}
