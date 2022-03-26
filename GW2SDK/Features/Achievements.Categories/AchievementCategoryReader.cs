using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    public sealed class AchievementCategoryReader : IJsonReader<AchievementCategory>
    {
        private readonly IJsonReader<AchievementRef> achievementRefReader;

        public AchievementCategoryReader(IJsonReader<AchievementRef> achievementRefReader)
        {
            this.achievementRefReader = achievementRefReader;
        }

        public AchievementCategory Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var description = new RequiredMember<string>("description");
            var order = new RequiredMember<int>("order");
            var icon = new RequiredMember<string>("icon");
            var achievements = new RequiredMember<IEnumerable<AchievementRef>>("achievements");
            var tomorrow = new OptionalMember<IEnumerable<AchievementRef>>("tomorrow");

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
                    achievements.Select(value => achievementRefReader.ReadArray(value, missingMemberBehavior)),
                Tomorrow = tomorrow.Select(value => achievementRefReader.ReadArray(value, missingMemberBehavior)) ??
                    Enumerable.Empty<AchievementRef>()
            };
        }
    }
}
