using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Treasury;

internal static class CountNeededForUpgradeJson
{
    public static CountNeededForUpgrade GetCountNeededForUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember upgradeId = "upgrade_id";
        RequiredMember count = "count";

        foreach (var member in json.EnumerateObject())
        {
            if (upgradeId.Match(member))
            {
                upgradeId = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CountNeededForUpgrade
        {
            UpgradeId = upgradeId.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32())
        };
    }
}
