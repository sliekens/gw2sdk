using System;
using System.Text.Json;
using GW2SDK.Home.Cats.Models;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Home.Cats.Json;

[PublicAPI]
public static class CatReader
{
    public static Cat Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> hint = new("hint");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(hint.Name))
            {
                hint = hint.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Cat
        {
            Id = id.GetValue(),
            Hint = hint.GetValue()
        };
    }
}
