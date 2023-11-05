using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Groups;

internal static class AchievementGroupJson
{
    public static AchievementGroup GetAchievementGroup(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember order = "order";
        RequiredMember categories = "categories";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == order.Name)
            {
                order = member;
            }
            else if (member.Name == categories.Name)
            {
                categories = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementGroup
        {
            Id = id.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Order = order.Map(value => value.GetInt32()),
            Categories = categories.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
