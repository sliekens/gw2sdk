using System.Text.Json;
using GuildWars2.Json;
using GuildWars2.Pvp.Games;

namespace GuildWars2.Guilds.Teams;

internal static class GameJson
{
    public static Game GetGame(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember mapId = "map_id";
        RequiredMember started = "started";
        RequiredMember ended = "ended";
        RequiredMember result = "result";
        RequiredMember team = "team";
        RequiredMember ratingType = "rating_type";
        OptionalMember ratingChange = "rating_change";
        OptionalMember seasonId = "season";
        RequiredMember score = "scores";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (mapId.Match(member))
            {
                mapId = member;
            }
            else if (started.Match(member))
            {
                started = member;
            }
            else if (ended.Match(member))
            {
                ended = member;
            }
            else if (result.Match(member))
            {
                result = member;
            }
            else if (team.Match(member))
            {
                team = member;
            }
            else if (ratingType.Match(member))
            {
                ratingType = member;
            }
            else if (ratingChange.Match(member))
            {
                ratingChange = member;
            }
            else if (seasonId.Match(member))
            {
                seasonId = member;
            }
            else if (score.Match(member))
            {
                score = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Game
        {
            Id = id.Map(static value => value.GetStringRequired()),
            MapId = mapId.Map(static value => value.GetInt32()),
            Started = started.Map(static value => value.GetDateTimeOffset()),
            Ended = ended.Map(static value => value.GetDateTimeOffset()),
            Result = result.Map(static value => value.GetEnum<PvpResult>()),
            Team = team.Map(static value => value.GetEnum<PvpTeamColor>()),
            RatingType = ratingType.Map(static value => value.GetRatingType()),
            RatingChange = ratingChange.Map(static value => value.GetInt32()),
            SeasonId = seasonId.Map(static value => value.GetString()),
            Score = score.Map(static value => value.GetScore())
        };
    }
}
