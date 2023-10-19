using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Dungeons;

[PublicAPI]
public static class DungeonJson
{
    public static Dungeon GetDungeon(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember paths = new("paths");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(paths.Name))
            {
                paths.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Dungeon
        {
            Id = id.Select(value => value.GetStringRequired()),
            Paths = paths.SelectMany(value => value.GetDungeonPath(missingMemberBehavior))
        };
    }
}
