using System;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Masteries.Models;
using JetBrains.Annotations;

namespace GW2SDK.Masteries.Json;

[PublicAPI]
public static class MasteryProgressReader
{
    public static MasteryProgress Read(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> level = new("level");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(level.Name))
            {
                level = level.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryProgress
        {
            Id = id.GetValue(),
            Level = level.GetValue()
        };
    }
}
