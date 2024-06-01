using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

internal static class UpgradeTierJson
{
    public static UpgradeTier GetUpgradeTier(this JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember yaksRequired = "yaks_required";
        RequiredMember upgrades = "upgrades";

        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (yaksRequired.Match(member))
            {
                yaksRequired = member;
            }
            else if (upgrades.Match(member))
            {
                upgrades = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new UpgradeTier
        {
            Name = name.Map(static value => value.GetStringRequired()),
            YaksRequired = yaksRequired.Map(static value => value.GetInt32()),
            Upgrades = upgrades.Map(
                static values => values.GetList(static value => value.GetUpgrade())
            )
        };
    }
}
