﻿using System.Text.Json;
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
        RequiredMember id = "id";
        RequiredMember paths = "paths";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(paths.Name))
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
            Id = id.Select(value => value.GetStringRequired()),
            Paths = paths.SelectMany(value => value.GetDungeonPath(missingMemberBehavior))
        };
    }
}
