using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

internal static class LevelJson
{
    public static Level GetLevel(this in JsonElement json)
    {
        RequiredMember minLevel = "min_rank";
        RequiredMember maxLevel = "max_rank";
        RequiredMember points = "points";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Level
        {
            MinRank = minLevel.Map(static (in value) => value.GetInt32()),
            MaxRank = maxLevel.Map(static (in value) => value.GetInt32()),
            Points = points.Map(static (in value) => value.GetInt32())
        };
    }
}
