﻿using System.Text.Json;
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
        RequiredMember min = new("min");
        RequiredMember max = new("max");

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
            Min = min.Select(value => value.GetInt32()),
            Max = max.Select(value => value.GetInt32())
        };
    }
}
