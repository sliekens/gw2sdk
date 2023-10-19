using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
public static class UpgradeJson
{
    public static Upgrade GetUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember icon = new("icon");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Upgrade
        {
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            IconHref = icon.Select(value => value.GetStringRequired())
        };
    }
}
