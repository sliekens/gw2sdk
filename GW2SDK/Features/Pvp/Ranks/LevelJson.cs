using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

internal static class LevelJson
{
    public static Level GetLevel(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember minLevel = "min_rank";
        RequiredMember maxLevel = "max_rank";
        RequiredMember points = "points";

        foreach (var member in json.EnumerateObject())
        {
            if (minLevel.Match(member))
            {
                minLevel = member;
            }
            else if (maxLevel.Match(member))
            {
                maxLevel = member;
            }
            else if (points.Match(member))
            {
                points = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Level
        {
            MinRank = minLevel.Map(value => value.GetInt32()),
            MaxRank = maxLevel.Map(value => value.GetInt32()),
            Points = points.Map(value => value.GetInt32())
        };
    }
}
