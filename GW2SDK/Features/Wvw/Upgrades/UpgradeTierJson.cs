using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

internal static class UpgradeTierJson
{
    public static UpgradeTier GetUpgradeTier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember yaksRequired = "yaks_required";
        RequiredMember upgrades = "upgrades";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(yaksRequired.Name))
            {
                yaksRequired = member;
            }
            else if (member.NameEquals(upgrades.Name))
            {
                upgrades = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UpgradeTier
        {
            Name = name.Map(value => value.GetStringRequired()),
            YaksRequired = yaksRequired.Map(value => value.GetInt32()),
            Upgrades = upgrades.Map(
                values => values.GetList(value => value.GetUpgrade(missingMemberBehavior))
            )
        };
    }
}
