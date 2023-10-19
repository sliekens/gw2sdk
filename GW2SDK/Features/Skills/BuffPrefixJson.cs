using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills;

[PublicAPI]
public static class BuffPrefixJson
{
    public static BuffPrefix GetBuffPrefix(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember text = new("text");
        RequiredMember icon = new("icon");
        OptionalMember status = new("status");
        OptionalMember description = new("description");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(text.Name))
            {
                text.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(status.Name))
            {
                status.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuffPrefix
        {
            Text = text.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Status = status.Select(value => value.GetString()) ?? "",
            Description = description.Select(value => value.GetString()) ?? ""
        };
    }
}
