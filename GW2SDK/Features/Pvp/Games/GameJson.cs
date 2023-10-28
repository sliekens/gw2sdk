using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Games;

internal static class GameJson
{
    public static Game GetGame(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember mapId = "map_id";
        RequiredMember started = "started";
        RequiredMember ended = "ended";
        RequiredMember result = "result";
        RequiredMember team = "team";
        RequiredMember profession = "profession";
        RequiredMember ratingType = "rating_type";
        OptionalMember ratingChange = "rating_change";
        OptionalMember seasonId = "season";
        RequiredMember score = "scores";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == mapId.Name)
            {
                mapId = member;
            }
            else if (member.Name == started.Name)
            {
                started = member;
            }
            else if (member.Name == ended.Name)
            {
                ended = member;
            }
            else if (member.Name == result.Name)
            {
                result = member;
            }
            else if (member.Name == team.Name)
            {
                team = member;
            }
            else if (member.Name == profession.Name)
            {
                profession = member;
            }
            else if (member.Name == ratingType.Name)
            {
                ratingType = member;
            }
            else if (member.Name == ratingChange.Name)
            {
                ratingChange = member;
            }
            else if (member.Name == seasonId.Name)
            {
                seasonId = member;
            }
            else if (member.Name == score.Name)
            {
                score = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Game
        {
            Id = id.Map(value => value.GetStringRequired()),
            MapId = mapId.Map(value => value.GetInt32()),
            Started = started.Map(value => value.GetDateTimeOffset()),
            Ended = ended.Map(value => value.GetDateTimeOffset()),
            Result = result.Map(value => value.GetEnum<PvpResult>(missingMemberBehavior)),
            Team = team.Map(value => value.GetEnum<PvpTeamColor>(missingMemberBehavior)),
            Profession =
                profession.Map(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            RatingType = ratingType.Map(value => value.GetRatingType(missingMemberBehavior)),
            RatingChange = ratingChange.Map(value => value.GetInt32()),
            SeasonId = seasonId.Map(value => value.GetString()),
            Score = score.Map(value => value.GetScore(missingMemberBehavior))
        };
    }
}
