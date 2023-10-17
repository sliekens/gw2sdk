using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Games;

[PublicAPI]
public static class GameJson
{
    public static Game GetGame(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<int> mapId = new("map_id");
        RequiredMember<DateTimeOffset> started = new("started");
        RequiredMember<DateTimeOffset> ended = new("ended");
        RequiredMember<GameResult> result = new("result");
        RequiredMember<TeamColor> team = new("team");
        RequiredMember<ProfessionName> profession = new("profession");
        RequiredMember<RatingType> ratingType = new("rating_type");
        OptionalMember<int> ratingChange = new("rating_change");
        OptionalMember<string> seasonId = new("season");
        RequiredMember<Score> score = new("scores");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(mapId.Name))
            {
                mapId.Value = member.Value;
            }
            else if (member.NameEquals(started.Name))
            {
                started.Value = member.Value;
            }
            else if (member.NameEquals(ended.Name))
            {
                ended.Value = member.Value;
            }
            else if (member.NameEquals(result.Name))
            {
                result.Value = member.Value;
            }
            else if (member.NameEquals(team.Name))
            {
                team.Value = member.Value;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession.Value = member.Value;
            }
            else if (member.NameEquals(ratingType.Name))
            {
                ratingType.Value = member.Value;
            }
            else if (member.NameEquals(ratingChange.Name))
            {
                ratingChange.Value = member.Value;
            }
            else if (member.NameEquals(seasonId.Name))
            {
                seasonId.Value = member.Value;
            }
            else if (member.NameEquals(score.Name))
            {
                score.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Game
        {
            Id = id.GetValue(),
            MapId = mapId.GetValue(),
            Started = started.GetValue(),
            Ended = ended.GetValue(),
            Result = result.GetValue(missingMemberBehavior),
            Team = team.GetValue(missingMemberBehavior),
            Profession = profession.GetValue(missingMemberBehavior),
            RatingType = ratingType.Select(value => value.GetRatingType(missingMemberBehavior)),
            RatingChange = ratingChange.GetValue(),
            SeasonId = seasonId.GetValueOrNull(),
            Score = score.Select(value => value.GetScore(missingMemberBehavior))
        };

    }
}
