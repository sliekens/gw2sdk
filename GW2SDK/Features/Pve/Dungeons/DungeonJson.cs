using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Dungeons;

internal static class DungeonJson
{
    public static Dungeon GetDungeon(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember paths = "paths";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (paths.Match(member))
            {
                paths = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Dungeon
        {
            Id = id.Map(value => value.GetStringRequired()),
            Paths = paths.Map(
                values => values.GetList(value => value.GetDungeonPath(missingMemberBehavior))
            )
        };
    }
}
