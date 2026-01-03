using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

internal static class GuildTeamJson
{
    public static GuildTeam GetGuildTeam(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember members = "members";
        RequiredMember name = "name";
        RequiredMember state = "state";
        RequiredMember aggregate = "aggregate";
        RequiredMember ladders = "ladders";
        RequiredMember games = "games";
        OptionalMember seasons = "seasons";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildTeam
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Members =
                members.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetGuildTeamMember())
                ),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            State = state.Map(static (in value) => value.GetEnum<GuildTeamState>()),
            Aggregate = aggregate.Map(static (in value) => value.GetResults()),
            Ladders = ladders.Map(static (in value) => value.GetLadders()),
            Games = games.Map(static (in values) => values.GetList(static (in value) => value.GetGame())),
            Seasons = seasons.Map(static (in values) => values.GetList(static (in value) => value.GetSeason())
                )
                ?? []
        };
    }
}
