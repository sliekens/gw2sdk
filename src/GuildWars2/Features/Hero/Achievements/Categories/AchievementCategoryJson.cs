using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

internal static class AchievementCategoryJson
{
    public static AchievementCategory GetAchievementCategory(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember achievements = "achievements";
        OptionalMember tomorrow = "tomorrow";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AchievementCategory
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired()),
            Order = order.Map(static (in value) => value.GetInt32()),
            IconUrl = new Uri(icon.Map(static (in value) => value.GetStringRequired()), UriKind.RelativeOrAbsolute),
            Achievements =
                achievements.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetAchievementRef())
                ),
            Tomorrow = tomorrow.Map(static (in values) =>
                values.GetList(static (in value) => value.GetAchievementRef())
            )
        };
    }
}
