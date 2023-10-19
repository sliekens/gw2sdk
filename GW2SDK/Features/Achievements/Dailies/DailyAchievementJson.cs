using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Dailies;

[PublicAPI]
public static class DailyAchievementJson
{
    public static DailyAchievement GetDailyAchievement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember level = "level";
        OptionalMember requiredAccess = "required_access";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (member.NameEquals(requiredAccess.Name))
            {
                requiredAccess = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DailyAchievement
        {
            Id = id.Map(value => value.GetInt32()),
            Level = level.Map(value => value.GetLevelRequirement(missingMemberBehavior)),
            RequiredAccess =
                requiredAccess.Map(value => value.GetProductRequirement(missingMemberBehavior))
        };
    }
}
