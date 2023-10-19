using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Categories;

[PublicAPI]
public static class AchievementCategoryJson
{
    public static AchievementCategory GetAchievementCategory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember order = new("order");
        RequiredMember icon = new("icon");
        RequiredMember achievements = new("achievements");
        OptionalMember tomorrow = new("tomorrow");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(order.Name))
            {
                order = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(achievements.Name))
            {
                achievements = member;
            }
            else if (member.NameEquals(tomorrow.Name))
            {
                tomorrow = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementCategory
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Order = order.Select(value => value.GetInt32()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Achievements =
                achievements.SelectMany(item => item.GetAchievementRef(missingMemberBehavior)),
            Tomorrow = tomorrow.SelectMany(item => item.GetAchievementRef(missingMemberBehavior))
        };
    }
}
