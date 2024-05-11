using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

internal static class AchievementRefJson
{
    public static AchievementRef GetAchievementRef(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementRef
        {
            Id = id.Map(static value => value.GetInt32()),
            Flags = flags.Map(static values => values.GetAchievementFlags())
                ?? new AchievementFlags
                {
                    SpecialEvent = false,
                    PvE = false,
                    Other = Empty.ListOfString
                },
            Level = level.Map(static value => value.GetLevelRequirement())
        };
    }
}
