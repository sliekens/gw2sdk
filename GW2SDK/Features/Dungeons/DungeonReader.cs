using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Dungeons;

[PublicAPI]
public static class DungeonReader
{
    public static Dungeon Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> id = new("id");
        RequiredMember<DungeonPath> paths = new("paths");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(paths.Name))
            {
                paths = paths.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Dungeon
        {
            Id = id.GetValue(),
            Paths = paths.SelectMany(value => ReadDungeonPath(value, missingMemberBehavior))
        };
    }

    private static DungeonPath ReadDungeonPath(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<DungeonKind> kind = new("type");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(kind.Name))
            {
                kind = kind.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DungeonPath
        {
            Id = id.GetValue(),
            Kind = kind.GetValue(missingMemberBehavior)
        };
    }
}
