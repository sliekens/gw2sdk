﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

[PublicAPI]
public static class GuildTeamJson
{
    public static GuildTeam GetGuildTeam(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember members = new("members");
        RequiredMember name = new("name");
        RequiredMember state = new("state");
        RequiredMember aggregate = new("aggregate");
        RequiredMember ladders = new("ladders");
        RequiredMember games = new("games");
        OptionalMember seasons = new("seasons");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(members.Name))
            {
                members = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(state.Name))
            {
                state = member;
            }
            else if (member.NameEquals(aggregate.Name))
            {
                aggregate = member;
            }
            else if (member.NameEquals(ladders.Name))
            {
                ladders = member;
            }
            else if (member.NameEquals(games.Name))
            {
                games = member;
            }
            else if (member.NameEquals(seasons.Name))
            {
                seasons = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildTeam
        {
            Id = id.Select(value => value.GetInt32()),
            Members = members.SelectMany(value => value.GetGuildTeamMember(missingMemberBehavior)),
            Name = name.Select(value => value.GetStringRequired()),
            State = state.Select(value => value.GetEnum<GuildTeamState>(missingMemberBehavior)),
            Aggregate = aggregate.Select(value => value.GetResults(missingMemberBehavior)),
            Ladders = ladders.Select(value => value.GetLadders(missingMemberBehavior)),
            Games = games.SelectMany(value => value.GetGame(missingMemberBehavior)),
            Seasons = seasons.SelectMany(value => value.GetSeason(missingMemberBehavior))
                ?? Array.Empty<Season>()
        };
    }
}
