using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Categories;

internal static class AchievementRefJson
{
    public static AchievementRef GetAchievementRef(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        OptionalMember requiredAccess = "required_access";
        OptionalMember flags = "flags";
        OptionalMember level = "level";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == requiredAccess.Name)
            {
                requiredAccess = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == level.Name)
            {
                level = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementRef
        {
            Id = id.Map(value => value.GetInt32()),
            Flags = flags.Map(values => values.GetAchievementFlags())
                ?? new AchievementFlags
                {
                    SpecialEvent = false,
                    PvE = false,
                    Other = Array.Empty<string>()
                },
            Level = level.Map(value => value.GetLevelRequirement(missingMemberBehavior))
        };
    }
}
