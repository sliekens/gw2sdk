using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
public static class ObjectiveUpgradeJson
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(tiers.Name))
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
            Tiers = tiers.Map(values => values.GetList(value => value.GetUpgradeTier(missingMemberBehavior)))
        };
    }
}
