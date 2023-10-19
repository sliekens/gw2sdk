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
        RequiredMember id = new("id");
        RequiredMember level = new("level");
        OptionalMember requiredAccess = new("required_access");

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
            Id = id.Select(value => value.GetInt32()),
            Level = level.Select(value => value.GetLevelRequirement(missingMemberBehavior)),
            RequiredAccess =
                requiredAccess.Select(value => value.GetProductRequirement(missingMemberBehavior))
        };
    }
}
