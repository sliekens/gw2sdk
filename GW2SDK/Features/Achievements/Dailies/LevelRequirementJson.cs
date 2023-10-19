using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Dailies;

[PublicAPI]
public static class LevelRequirementJson
{
    public static LevelRequirement GetLevelRequirement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember min = "min";
        RequiredMember max = "max";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(min.Name))
            {
                min = member;
            }
            else if (member.NameEquals(max.Name))
            {
                max = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LevelRequirement
        {
            Min = min.Map(value => value.GetInt32()),
            Max = max.Map(value => value.GetInt32())
        };
    }
}
