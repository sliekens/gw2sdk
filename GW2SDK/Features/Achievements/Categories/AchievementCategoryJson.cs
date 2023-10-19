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
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember achievements = "achievements";
        OptionalMember tomorrow = "tomorrow";

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
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Order = order.Map(value => value.GetInt32()),
            Icon = icon.Map(value => value.GetStringRequired()),
            Achievements =
                achievements.Map(
                    values => values.GetList(item => item.GetAchievementRef(missingMemberBehavior))
                ),
            Tomorrow = tomorrow.Map(
                values => values.GetList(item => item.GetAchievementRef(missingMemberBehavior))
            )
        };
    }
}
