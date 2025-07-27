using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Treasury;

internal static class GuildTreasurySlotJson
{
    public static GuildTreasurySlot GetGuildTreasurySlot(this in JsonElement json)
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildTreasurySlot
        {
            ItemId = itemId.Map(static (in JsonElement value) => value.GetInt32()),
            Count = count.Map(static (in JsonElement value) => value.GetInt32()),
            CountNeededForUpgrades = countNeededForUpgrade.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetCountNeededForUpgrade())
            )
        };
    }
}
