using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Groups;

[PublicAPI]
public static class AchievementGroupJson
{
    public static AchievementGroup GetAchievementGroup(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember order = new("order");
        RequiredMember categories = new("categories");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (member.NameEquals(categories.Name))
            {
                categories.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementGroup
        {
            Id = id.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Order = order.Select(value => value.GetInt32()),
            Categories = categories.SelectMany(value => value.GetInt32())
        };
    }
}
