using System;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Meta.Models;
using JetBrains.Annotations;

namespace GW2SDK.Meta.Json;

[PublicAPI]
public static class BuildReader
{
    public static Build Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Build
        {
            Id = id.GetValue()
        };
    }
}
