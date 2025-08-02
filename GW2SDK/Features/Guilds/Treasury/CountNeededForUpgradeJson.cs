using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Treasury;

internal static class CountNeededForUpgradeJson
{
    public static CountNeededForUpgrade GetCountNeededForUpgrade(this in JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new CountNeededForUpgrade
        {
            UpgradeId = upgradeId.Map(static (in JsonElement value) => value.GetInt32()),
            Count = count.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
