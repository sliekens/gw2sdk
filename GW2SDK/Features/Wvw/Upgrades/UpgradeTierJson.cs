using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
public static class UpgradeTierJson
{
    public static UpgradeTier GetUpgradeTier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        RequiredMember yaksRequired = new("yaks_required");
        RequiredMember upgrades = new("upgrades");

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
            Name = name.Select(value => value.GetStringRequired()),
            YaksRequired = yaksRequired.Select(value => value.GetInt32()),
            Upgrades = upgrades.SelectMany(value => value.GetUpgrade(missingMemberBehavior))
        };
    }
}
