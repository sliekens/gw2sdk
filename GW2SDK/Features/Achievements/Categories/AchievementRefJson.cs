using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Categories;

[PublicAPI]
public static class AchievementRefJson
{
    public static AchievementRef GetAchievementRef(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        OptionalMember requiredAccess = new("required_access");
        OptionalMember flags = new("flags");
        OptionalMember level = new("level");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(requiredAccess.Name))
            {
                requiredAccess.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementRef
        {
            Id = id.Select(value => value.GetInt32()),
            RequiredAccess =
                requiredAccess.Select(value => value.GetProductRequirement(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<AchievementFlag>(missingMemberBehavior)),
            Level = level.Select(value => value.GetLevelRequirement(missingMemberBehavior))
        };
    }
}
