using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

internal static class GuildTeamJson
{
    public static GuildTeam GetGuildTeam(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember members = "members";
        RequiredMember name = "name";
        RequiredMember state = "state";
        RequiredMember aggregate = "aggregate";
        RequiredMember ladders = "ladders";
        RequiredMember games = "games";
        OptionalMember seasons = "seasons";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (members.Match(member))
            {
                members = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (state.Match(member))
            {
                state = member;
            }
            else if (aggregate.Match(member))
            {
                aggregate = member;
            }
            else if (ladders.Match(member))
            {
                ladders = member;
            }
            else if (games.Match(member))
            {
                games = member;
            }
            else if (seasons.Match(member))
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
            Id = id.Map(value => value.GetInt32()),
            Members =
                members.Map(
                    values => values.GetList(
                        value => value.GetGuildTeamMember(missingMemberBehavior)
                    )
                ),
            Name = name.Map(value => value.GetStringRequired()),
            State = state.Map(value => value.GetEnum<GuildTeamState>(missingMemberBehavior)),
            Aggregate = aggregate.Map(value => value.GetResults(missingMemberBehavior)),
            Ladders = ladders.Map(value => value.GetLadders(missingMemberBehavior)),
            Games = games.Map(
                values => values.GetList(value => value.GetGame(missingMemberBehavior))
            ),
            Seasons = seasons.Map(
                    values => values.GetList(value => value.GetSeason(missingMemberBehavior))
                )
                ?? Empty.List<Season>()
        };
    }
}
