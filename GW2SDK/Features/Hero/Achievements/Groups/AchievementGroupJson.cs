using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Groups;

internal static class AchievementGroupJson
{
    public static AchievementGroup GetAchievementGroup(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember order = "order";
        RequiredMember categories = "categories";
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
            else if (categories.Match(member))
            {
                categories = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AchievementGroup
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired()),
            Order = order.Map(static value => value.GetInt32()),
            Categories =
                categories.Map(static values => values.GetList(static value => value.GetInt32()))
        };
    }
}
