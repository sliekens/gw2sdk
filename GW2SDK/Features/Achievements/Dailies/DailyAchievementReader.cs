using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies;

[PublicAPI]
public static class DailyAchievementReader
{
    public static DailyAchievementGroup GetDailyAchievementGroup(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<DailyAchievement> pve = new("pve");
        RequiredMember<DailyAchievement> pvp = new("pvp");
        RequiredMember<DailyAchievement> wvw = new("wvw");
        RequiredMember<DailyAchievement> fractals = new("fractals");
        RequiredMember<DailyAchievement> special = new("special");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(pve.Name))
            {
                pve = pve.From(member.Value);
            }
            else if (member.NameEquals(pvp.Name))
            {
                pvp = pvp.From(member.Value);
            }
            else if (member.NameEquals(wvw.Name))
            {
                wvw = wvw.From(member.Value);
            }
            else if (member.NameEquals(fractals.Name))
            {
                fractals = fractals.From(member.Value);
            }
            else if (member.NameEquals(special.Name))
            {
                special = special.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DailyAchievementGroup
        {
            Pve = pve.SelectMany(value => ReadDailyAchievement(value, missingMemberBehavior)),
            Pvp = pvp.SelectMany(value => ReadDailyAchievement(value, missingMemberBehavior)),
            Wvw = wvw.SelectMany(value => ReadDailyAchievement(value, missingMemberBehavior)),
            Fractals =
                fractals.SelectMany(
                    value => ReadDailyAchievement(value, missingMemberBehavior)
                ),
            Special = special.SelectMany(
                value => ReadDailyAchievement(value, missingMemberBehavior)
            )
        };
    }

    private static DailyAchievement ReadDailyAchievement(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<LevelRequirement> level = new("level");
        OptionalMember<ProductRequirement> requiredAccess = new("required_access");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(level.Name))
            {
                level = level.From(member.Value);
            }
            else if (member.NameEquals(requiredAccess.Name))
            {
                requiredAccess = requiredAccess.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DailyAchievement
        {
            Id = id.GetValue(),
            Level = level.Select(value => ReadLevelRequirement(value, missingMemberBehavior)),
            RequiredAccess =
                requiredAccess.Select(value => ReadProductRequirement(value, missingMemberBehavior))
        };
    }

    private static LevelRequirement ReadLevelRequirement(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> min = new("min");
        RequiredMember<int> max = new("max");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(min.Name))
            {
                min = min.From(member.Value);
            }
            else if (member.NameEquals(max.Name))
            {
                max = max.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LevelRequirement
        {
            Min = min.GetValue(),
            Max = max.GetValue()
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
}
