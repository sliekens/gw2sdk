using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

[PublicAPI]
public static class AccountAchievementJson
{
    public static AccountAchievement GetAccountAchievement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
                id.Value = member.Value;
            }
            else if (member.NameEquals(current.Name))
            {
                current.Value = member.Value;
            }
            else if (member.NameEquals(max.Name))
            {
                max.Value = member.Value;
            }
            else if (member.NameEquals(done.Name))
            {
                done.Value = member.Value;
            }
            else if (member.NameEquals(bits.Name))
            {
                bits.Value = member.Value;
            }
            else if (member.NameEquals(repeated.Name))
            {
                repeated.Value = member.Value;
            }
            else if (member.NameEquals(unlocked.Name))
            {
                unlocked.Value = member.Value;
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
