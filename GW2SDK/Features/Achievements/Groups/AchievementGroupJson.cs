using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Groups;

[PublicAPI]
public static class AchievementGroupJson
{
    public static AchievementGroup GetAchievementGroup(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<int> order = new("order");
        RequiredMember<int> categories = new("categories");
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
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            Order = order.GetValue(),
            Categories = categories.SelectMany(value => value.GetInt32())
        };
    }
}
