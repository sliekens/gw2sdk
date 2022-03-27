using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    public sealed class DailyAchievementReader : IDailyAchievementReader
    {
        public DailyAchievementGroup Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var pve = new RequiredMember<DailyAchievement>("pve");
            var pvp = new RequiredMember<DailyAchievement>("pvp");
            var wvw = new RequiredMember<DailyAchievement>("wvw");
            var fractals = new RequiredMember<DailyAchievement>("fractals");
            var special = new RequiredMember<DailyAchievement>("special");

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
                Fractals = fractals.SelectMany(value => ReadDailyAchievement(value, missingMemberBehavior)),
                Special = special.SelectMany(value => ReadDailyAchievement(value, missingMemberBehavior))
            };
        }

        private DailyAchievement ReadDailyAchievement(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var level = new RequiredMember<LevelRequirement>("level");
            var requiredAccess = new OptionalMember<ProductRequirement>("required_access");

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
                RequiredAccess = requiredAccess.Select(value => ReadProductRequirement(value, missingMemberBehavior))
            };
        }

        private LevelRequirement ReadLevelRequirement(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var min = new RequiredMember<int>("min");
            var max = new RequiredMember<int>("max");

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

        private ProductRequirement ReadProductRequirement(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var product = new RequiredMember<ProductName>("product");
            var condition = new RequiredMember<AccessCondition>("condition");

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
}
