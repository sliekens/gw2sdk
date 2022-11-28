using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Dungeons;

[PublicAPI]
public static class DungeonJson
{
    public static Dungeon GetDungeon(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<DungeonPath> paths = new("paths");

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
            Id = id.GetValue(),
            Paths = paths.SelectMany(value => value.GetDungeonPath(missingMemberBehavior))
        };
    }
}
