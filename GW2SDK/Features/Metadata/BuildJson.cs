using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Metadata;

internal static class BuildJson
{
    public static Build GetBuild(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Build { Id = id.Map(value => value.GetInt32()) };
    }
}
