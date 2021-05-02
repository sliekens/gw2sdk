using System;
using System.Text.Json;
using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    public sealed class AchievementCategoryReader : IAchievementCategoryReader
    {
        public AchievementCategory Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var description = new RequiredMember<string>("description");
            var order = new RequiredMember<int>("order");
            var icon = new RequiredMember<string>("icon");
            var achievements = new RequiredMember<int[]>("achievements");

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
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new AchievementCategory
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Description = description.GetValue(),
                Order = order.GetValue(),
                Icon = icon.GetValue(),
                Achievements = achievements.Select(value => value.GetArray(item => item.GetInt32()))
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
