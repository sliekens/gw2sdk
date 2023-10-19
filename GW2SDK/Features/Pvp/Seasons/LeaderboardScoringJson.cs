﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class LeaderboardScoringJson
{
    public static LeaderboardScoring GetLeaderboardScoring(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember type = "type";
        RequiredMember description = "description";
        RequiredMember name = "name";
        RequiredMember ordering = "ordering";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(type.Name))
            {
                type = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(ordering.Name))
            {
                ordering = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardScoring
        {
            Id = id.Select(value => value.GetStringRequired()),
            Type = type.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Name = type.Select(value => value.GetStringRequired()),
            Ordering = ordering.Select(value => value.GetStringRequired())
        };
    }
}
