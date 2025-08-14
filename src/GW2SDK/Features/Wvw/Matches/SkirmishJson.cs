using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class SkirmishJson
{
    public static Skirmish GetSkirmish(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember scores = "scores";
        OptionalMember mapScores = "map_scores";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (scores.Match(member))
            {
                scores = member;
            }
            else if (mapScores.Match(member))
            {
                mapScores = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Skirmish
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Scores = scores.Map(static (in JsonElement value) => value.GetDistribution()),
            MapScores =
                mapScores.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetMapScores()))
                ?? []
        };
    }
}
