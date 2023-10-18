using System.Text.Json;
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
        RequiredMember<int> id = new("id");
        RequiredMember<GuildTeamMember> members = new("members");
        RequiredMember<string> name = new("name");
        RequiredMember<GuildTeamState> state = new("state");
        RequiredMember<Results> aggregate = new("aggregate");
        RequiredMember<Ladders> ladders = new("ladders");
        RequiredMember<Game> games = new("games");
        OptionalMember<Season> seasons = new("seasons");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(members.Name))
            {
                members.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(state.Name))
            {
                state.Value = member.Value;
            }
            else if (member.NameEquals(aggregate.Name))
            {
                aggregate.Value = member.Value;
            }
            else if (member.NameEquals(ladders.Name))
            {
                ladders.Value = member.Value;
            }
            else if (member.NameEquals(games.Name))
            {
                games.Value = member.Value;
            }
            else if (member.NameEquals(seasons.Name))
            {
                seasons.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildTeam
        {
            Id = id.GetValue(),
            Members = members.SelectMany(value => value.GetGuildTeamMember(missingMemberBehavior)),
            Name = name.GetValue(),
            State = state.GetValue(missingMemberBehavior),
            Aggregate = aggregate.Select(value => value.GetResults(missingMemberBehavior)),
            Ladders = ladders.Select(value => value.GetLadders(missingMemberBehavior)),
            Games = games.SelectMany(value => value.GetGame(missingMemberBehavior)),
            Seasons = seasons.SelectMany(value => value.GetSeason(missingMemberBehavior))
                ?? Array.Empty<Season>()
        };
    }
}
