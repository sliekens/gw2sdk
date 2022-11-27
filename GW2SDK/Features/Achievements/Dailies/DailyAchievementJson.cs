using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies;

[PublicAPI]
public static class DailyAchievementJson
{
    public static DailyAchievement GetDailyAchievement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<LevelRequirement> level = new("level");
        OptionalMember<ProductRequirement> requiredAccess = new("required_access");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(requiredAccess.Name))
            {
                requiredAccess.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DailyAchievement
        {
            Id = id.GetValue(),
            Level = level.Select(value => value.GetLevelRequirement(missingMemberBehavior)),
            RequiredAccess =
                requiredAccess.Select(value => value.GetProductRequirement(missingMemberBehavior))
        };
    }
}
