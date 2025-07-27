using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Nodes;

internal static class NodeJson
{
    public static Node GetNode(this in JsonElement json)
    {
        RequiredMember id = "id";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Node { Id = id.Map(static (in JsonElement value) => value.GetStringRequired()) };
    }
}
