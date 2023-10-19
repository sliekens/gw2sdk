using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Dungeons;

[PublicAPI]
public static class DungeonPathJson
{
    public static DungeonPath GetDungeonPath(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember kind = "type";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(kind.Name))
            {
                kind = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DungeonPath
        {
            Id = id.Select(value => value.GetStringRequired()),
            Kind = kind.Select(value => value.GetEnum<DungeonKind>(missingMemberBehavior))
        };
    }
}
