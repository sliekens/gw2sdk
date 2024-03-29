﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.HeroChallenges;

internal static class HeroChallengeJson
{
    public static HeroChallenge GetHeroChallenge(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember coordinates = "coord";

        // The 'id' is missing from hero points in End of Dragon maps
        OptionalMember id = "id";
        foreach (var member in json.EnumerateObject())
        {
            if (coordinates.Match(member))
            {
                coordinates = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HeroChallenge
        {
            Id = id.Map(value => value.GetString()) ?? "",
            Coordinates = coordinates.Map(value => value.GetCoordinateF(missingMemberBehavior))
        };
    }
}
