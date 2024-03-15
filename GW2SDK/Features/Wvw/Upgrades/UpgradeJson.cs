using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

internal static class UpgradeJson
{
    public static Upgrade GetUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember icon = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Upgrade
        {
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired())
        };
    }
}
