using System;
using System.Text.Json;

using GW2SDK.Achievements.Models;
using GW2SDK.Json;

using JetBrains.Annotations;

namespace GW2SDK.Achievements.Json;

[PublicAPI]
public static class AccountAchievementReader
{
    public static AccountAchievement Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> current = new("current");
        RequiredMember<int> max = new("max");
        RequiredMember<bool> done = new("done");
        OptionalMember<int> bits = new("bits");
        NullableMember<int> repeated = new("repeated");
        NullableMember<bool> unlocked = new("unlocked");

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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AccountAchievement
        {
            Id = id.GetValue(),
            Current = current.GetValue(),
            Max = max.GetValue(),
            Done = done.GetValue(),
            Bits = bits.SelectMany(value => value.GetInt32()),
            Repeated = repeated.GetValue(),
            Unlocked = unlocked.GetValue()
        };
    }
}