using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Titles
{
    [PublicAPI]
    public sealed class TitleReader : ITitleReader
    {
        public Title Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var achievements = new OptionalMember<int[]>("achievements");
            var achievementPointsRequired = new NullableMember<int>("ap_required");

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
                else if (member.NameEquals(achievements.Name))
                {
                    achievements = achievements.From(member.Value);
                }
                else if (member.NameEquals(achievementPointsRequired.Name))
                {
                    achievementPointsRequired = achievementPointsRequired.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    if (member.NameEquals("achievement"))
                    {
                        // Obsolete because some titles can be unlocked by more than one achievement
                        continue;
                    }

                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Title
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Achievements = achievements.Select(value => ReadAchievements(value)),
                AchievementPointsRequired = achievementPointsRequired.GetValue()
            };
        }

        private int[] ReadAchievements(JsonElement value)
        {
            return value.GetArray(item => item.GetInt32());
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
