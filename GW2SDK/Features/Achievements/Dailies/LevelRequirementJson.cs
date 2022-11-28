using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Achievements.Dailies;

[PublicAPI]
public static class LevelRequirementJson
{
    public static LevelRequirement GetLevelRequirement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> min = new("min");
        RequiredMember<int> max = new("max");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(min.Name))
            {
                min.Value = member.Value;
            }
            else if (member.NameEquals(max.Name))
            {
                max.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LevelRequirement
        {
            Min = min.GetValue(),
            Max = max.GetValue()
        };
    }
}
