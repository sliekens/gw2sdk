using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

internal static class UpgradeJson
{
    public static Upgrade GetUpgrade(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember icon = "icon";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());

        return new Upgrade
        {
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString)
        };
    }
}
