using System;
using System.Text.Json;
using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Accounts.Achievements
{
    [PublicAPI]
    public sealed class AccountAchievementReader : IAccountAchievementReader
    {
        public AccountAchievement Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var id = new RequiredMember<int>("id");
            var current = new RequiredMember<int>("current");
            var max = new RequiredMember<int>("max");
            var done = new RequiredMember<bool>("done");
            var bits = new OptionalMember<int[]>("bits");
            var repeated = new NullableMember<int>("repeated");
            var unlocked = new NullableMember<bool>("unlocked");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(current.Name))
                {
                    current = current.From(member.Value);
                }
                else if (member.NameEquals(max.Name))
                {
                    max = max.From(member.Value);
                }
                else if (member.NameEquals(done.Name))
                {
                    done = done.From(member.Value);
                }
                else if (member.NameEquals(bits.Name))
                {
                    bits = bits.From(member.Value);
                }
                else if (member.NameEquals(repeated.Name))
                {
                    repeated = repeated.From(member.Value);
                }
                else if (member.NameEquals(unlocked.Name))
                {
                    unlocked = unlocked.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new AccountAchievement
            {
                Id = id.GetValue(),
                Current = current.GetValue(),
                Max = max.GetValue(),
                Done = done.GetValue(),
                Bits = bits.Select(value => value.GetArray(item => item.GetInt32())),
                Repeated = repeated.GetValue(),
                Unlocked = unlocked.GetValue()
            };
        }
    }
}
