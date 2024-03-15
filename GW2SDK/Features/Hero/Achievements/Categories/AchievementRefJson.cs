using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

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
            if (id.Match(member))
            {
                id = member;
            }
            else if (requiredAccess.Match(member))
            {
                requiredAccess = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (level.Match(member))
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
                    Other = Empty.ListOfString
                },
            Level = level.Map(value => value.GetLevelRequirement(missingMemberBehavior))
        };
    }
}
