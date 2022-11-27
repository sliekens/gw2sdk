using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public static class BuffPrefixJson
{
    public static BuffPrefix GetBuffPrefix(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        OptionalMember<string> status = new("status");
        OptionalMember<string> description = new("description");
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
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Status = status.GetValueOrEmpty(),
            Description = description.GetValueOrEmpty()
        };
    }
}
