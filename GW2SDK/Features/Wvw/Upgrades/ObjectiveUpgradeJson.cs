using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

internal static class ObjectiveUpgradeJson
{
    public static ObjectiveUpgrade GetObjectiveUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember tiers = "tiers";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (tiers.Match(member))
            {
                tiers = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ObjectiveUpgrade
        {
            Id = id.Map(value => value.GetInt32()),
            Tiers = tiers.Map(
                values => values.GetList(value => value.GetUpgradeTier(missingMemberBehavior))
            )
        };
    }
}
