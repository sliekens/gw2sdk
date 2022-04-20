using System;
using System.Text.Json;
using GW2SDK.Home.Nodes.Models;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Home.Nodes.Json;

[PublicAPI]
public static class NodeReader
{
    public static Node Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> id = new("id");

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

        return new Node { Id = id.GetValue() };
    }
}
