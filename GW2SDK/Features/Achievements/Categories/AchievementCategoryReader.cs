using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Categories;

[PublicAPI]
public static class AchievementCategoryReader
{
    public static AchievementCategory GetAchievementCategory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<int> order = new("order");
        RequiredMember<string> icon = new("icon");
        RequiredMember<AchievementRef> achievements = new("achievements");
        OptionalMember<AchievementRef> tomorrow = new("tomorrow");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (member.NameEquals(order.Name))
            {
                order = order.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(achievements.Name))
            {
                achievements = achievements.From(member.Value);
            }
            else if (member.NameEquals(tomorrow.Name))
            {
                tomorrow = tomorrow.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementCategory
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            Order = order.GetValue(),
            Icon = icon.GetValue(),
            Achievements =
                achievements.SelectMany(item => ReadAchievementRef(item, missingMemberBehavior)),
            Tomorrow = tomorrow.SelectMany(item => ReadAchievementRef(item, missingMemberBehavior))
        };
    }

    private static AchievementRef ReadAchievementRef(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        OptionalMember<ProductRequirement> requiredAccess = new("required_access");
        OptionalMember<AchievementFlag> flags = new("flags");
        OptionalMember<LevelRequirement> level = new("level");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(requiredAccess.Name))
            {
                requiredAccess = requiredAccess.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(level.Name))
            {
                level = level.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementRef
        {
            Id = id.GetValue(),
            RequiredAccess =
                requiredAccess.Select(
                    value => ReadProductRequirement(value, missingMemberBehavior)
                    ),
            Flags = flags.GetValues(missingMemberBehavior),
            Level = level.Select(value => ReadLevel(value, missingMemberBehavior))
        };
    }

    private static ProductRequirement ReadProductRequirement(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<ProductName> product = new("product");
        RequiredMember<AccessCondition> condition = new("condition");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(product.Name))
            {
                product = product.From(member.Value);
            }
            else if (member.NameEquals(condition.Name))
            {
                condition = condition.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ProductRequirement
        {
            Product = product.GetValue(missingMemberBehavior),
            Condition = condition.GetValue(missingMemberBehavior)
        };
    }

    private static LevelRequirement ReadLevel(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var min = json[0].GetInt32();
        var max = json[1].GetInt32();
        return new LevelRequirement
        {
            Min = min,
            Max = max
        };
    }
}
