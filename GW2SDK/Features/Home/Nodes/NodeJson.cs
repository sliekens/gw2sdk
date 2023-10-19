using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Home.Nodes;

[PublicAPI]
public static class NodeJson
{
    public static Node GetNode(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Node { Id = id.Select(value => value.GetStringRequired()) };
    }
}
