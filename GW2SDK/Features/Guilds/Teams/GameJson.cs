using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

[PublicAPI]
public static class GameJson
{
    public static Game GetGame(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            Id = id.Select(value => value.GetStringRequired()),
            MapId = mapId.Select(value => value.GetInt32()),
            Started = started.Select(value => value.GetDateTimeOffset()),
            Ended = ended.Select(value => value.GetDateTimeOffset()),
            Result = result.Select(value => value.GetEnum<PvpResult>(missingMemberBehavior)),
            Team = team.Select(value => value.GetEnum<PvpTeamColor>(missingMemberBehavior)),
            RatingType = ratingType.Select(value => value.GetRatingType(missingMemberBehavior)),
            RatingChange = ratingChange.Select(value => value.GetInt32()),
            SeasonId = seasonId.Select(value => value.GetString()),
            Score = score.Select(value => value.GetScore(missingMemberBehavior))
        };

    }
}
