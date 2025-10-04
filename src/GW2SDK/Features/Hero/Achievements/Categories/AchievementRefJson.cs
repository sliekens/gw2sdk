using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

internal static class AchievementRefJson
{
    public static AchievementRef GetAchievementRef(this in JsonElement json)
    {
        RequiredMember id = "id";
        OptionalMember requiredAccess = "required_access";
        OptionalMember flags = "flags";
        OptionalMember level = "level";

        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AchievementRef
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Flags = flags.Map(static (in values) => values.GetAchievementFlags())
                ?? AchievementFlags.None,
            Level = level.Map(static (in value) => value.GetLevelRequirement())
        };
    }
}
