using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Dungeons;

internal static class DungeonPathJson
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (kind.Match(member))
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
            Id = id.Map(value => value.GetStringRequired()),
            Kind = kind.Map(value => value.GetEnum<DungeonKind>())
        };
    }
}
