using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
public static class LevelJson
{
    public static Level GetLevel(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember minLevel = new("min_rank");
        RequiredMember maxLevel = new("max_rank");
        RequiredMember points = new("points");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(minLevel.Name))
            {
                minLevel = member;
            }
            else if (member.NameEquals(maxLevel.Name))
            {
                maxLevel = member;
            }
            else if (member.NameEquals(points.Name))
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
            MinRank = minLevel.Select(value => value.GetInt32()),
            MaxRank = maxLevel.Select(value => value.GetInt32()),
            Points = points.Select(value => value.GetInt32())
        };
    }
}
