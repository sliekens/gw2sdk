using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Metadata;

internal static class BuildJson
{
    public static Build GetBuild(this in JsonElement json)
    {
        RequiredMember id = "id";

        foreach (JsonProperty member in json.EnumerateObject())
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

        return new Build { Id = id.Map(static (in value) => value.GetInt32()) };
    }
}
