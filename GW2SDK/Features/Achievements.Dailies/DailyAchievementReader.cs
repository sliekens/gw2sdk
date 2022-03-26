using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    public sealed class DailyAchievementReader : IDailyAchievementReader
    {
        public DailyAchievementGroup Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var pve = new RequiredMember<DailyAchievement[]>("pve");
            var pvp = new RequiredMember<DailyAchievement[]>("pvp");
            var wvw = new RequiredMember<DailyAchievement[]>("wvw");
            var fractals = new RequiredMember<DailyAchievement[]>("fractals");
            var special = new RequiredMember<DailyAchievement[]>("special");

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
                Pve = pve.Select(value => value.GetArray(item => ReadDailyAchievement(item, missingMemberBehavior))),
                Pvp = pvp.Select(value => value.GetArray(item => ReadDailyAchievement(item, missingMemberBehavior))),
                Wvw = wvw.Select(value => value.GetArray(item => ReadDailyAchievement(item, missingMemberBehavior))),
                Fractals = fractals.Select(value => value.GetArray(item => ReadDailyAchievement(item, missingMemberBehavior))),
                Special = special.Select(value => value.GetArray(item => ReadDailyAchievement(item, missingMemberBehavior)))
            };
        }

        private DailyAchievement ReadDailyAchievement(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var level = new RequiredMember<DailyAchievementLevelRequirement>("level");
            var requiredAccess = new OptionalMember<ProductName[]>("required_access");

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
                RequiredAccess = requiredAccess.GetValue(missingMemberBehavior) ?? Array.Empty<ProductName>()
            };
        }

        private DailyAchievementLevelRequirement ReadLevelRequirement(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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

            return new DailyAchievementLevelRequirement
            {
                Min = min.GetValue(),
                Max = max.GetValue()
            };
        }
    }
}
