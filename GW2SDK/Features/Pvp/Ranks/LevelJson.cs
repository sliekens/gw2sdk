using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
public static class LevelJson
{
    public static Level GetLevel(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> minLevel = new("min_rank");
        RequiredMember<int> maxLevel = new("max_rank");
        RequiredMember<int> points = new("points");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(minLevel.Name))
            {
                minLevel.Value = member.Value;
            }
            else if (member.NameEquals(maxLevel.Name))
            {
                maxLevel.Value = member.Value;
            }
            else if (member.NameEquals(points.Name))
            {
                points.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Level
        {
            MinRank = minLevel.GetValue(),
            MaxRank = maxLevel.GetValue(),
            Points = points.GetValue()
        };
    }
}
