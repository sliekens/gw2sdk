using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

[PublicAPI]
public static class GameJson
{
    public static Game GetGame(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(mapId.Name))
            {
                mapId = member;
            }
            else if (member.NameEquals(started.Name))
            {
                started = member;
            }
            else if (member.NameEquals(ended.Name))
            {
                ended = member;
            }
            else if (member.NameEquals(result.Name))
            {
                result = member;
            }
            else if (member.NameEquals(team.Name))
            {
                team = member;
            }
            else if (member.NameEquals(ratingType.Name))
            {
                ratingType = member;
            }
            else if (member.NameEquals(ratingChange.Name))
            {
                ratingChange = member;
            }
            else if (member.NameEquals(seasonId.Name))
            {
                seasonId = member;
            }
            else if (member.NameEquals(score.Name))
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
            RatingType = ratingType.Map(value => value.GetRatingType(missingMemberBehavior)),
            RatingChange = ratingChange.Map(value => value.GetInt32()),
            SeasonId = seasonId.Map(value => value.GetString()),
            Score = score.Map(value => value.GetScore(missingMemberBehavior))
        };
    }
}
