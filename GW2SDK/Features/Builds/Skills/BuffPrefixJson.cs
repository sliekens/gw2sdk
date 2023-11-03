using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Builds.Skills;

internal static class BuffPrefixJson
{
    public static BuffPrefix GetBuffPrefix(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember text = "text";
        RequiredMember icon = "icon";
        OptionalMember status = "status";
        OptionalMember description = "description";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == text.Name)
            {
                text = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == status.Name)
            {
                status = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuffPrefix
        {
            Text = text.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetStringRequired()),
            Status = status.Map(value => value.GetString()) ?? "",
            Description = description.Map(value => value.GetString()) ?? ""
        };
    }
}
