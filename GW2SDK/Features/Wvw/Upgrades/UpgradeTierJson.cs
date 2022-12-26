using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
public static class UpgradeTierJson
{
    public static UpgradeTier GetUpgradeTier(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<int> yaksRequired = new("yaks_required");
        RequiredMember<UpgradeTierUpgrade> upgrades = new("upgrades");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(yaksRequired.Name))
            {
                yaksRequired.Value = member.Value;
            }
            else if (member.NameEquals(upgrades.Name))
            {
                upgrades.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UpgradeTier
        {
            Name = name.GetValue(),
            YaksRequired = yaksRequired.GetValue(),
            Upgrades = upgrades.SelectMany(value => value.GetUpgradeTierUpgrade(missingMemberBehavior))
        };
    }
}
