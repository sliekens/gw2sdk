using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

internal static class AchievementCategoryJson
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (achievements.Match(member))
            {
                achievements = member;
            }
            else if (tomorrow.Match(member))
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
            IconHref = icon.Map(value => value.GetStringRequired()),
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
