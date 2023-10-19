using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Treasury;

[PublicAPI]
public static class CountNeededForUpgradeJson
{
    public static CountNeededForUpgrade GetCountNeededForUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember upgradeId = new("upgrade_id");
        RequiredMember count = new("count");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(upgradeId.Name))
            {
                upgradeId = member;
            }
            else if (member.NameEquals(count.Name))
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
            UpgradeId = upgradeId.Select(value => value.GetInt32()),
            Count = count.Select(value => value.GetInt32())
        };
    }
}
