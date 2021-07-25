using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    public sealed class AchievementGroupReader : IAchievementGroupReader
    {
        public IJsonReader<string> Id { get; } = new StringJsonReader();

        public AchievementGroup Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<string>("id");
            var name = new RequiredMember<string>("name");
            var description = new RequiredMember<string>("description");
            var order = new RequiredMember<int>("order");
            var categories = new RequiredMember<int[]>("categories");
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
                else if (member.NameEquals(categories.Name))
                {
                    categories = categories.From(member.Value);
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
                Categories = categories.Select(value => value.GetArray(item => item.GetInt32()))
            };
        }
    }
}
