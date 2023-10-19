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
        RequiredMember id = new("id");
        RequiredMember current = new("current");
        RequiredMember max = new("max");
        RequiredMember done = new("done");
        OptionalMember bits = new("bits");
        NullableMember repeated = new("repeated");
        NullableMember unlocked = new("unlocked");

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
            Id = id.Select(value => value.GetInt32()),
            Current = current.Select(value => value.GetInt32()),
            Max = max.Select(value => value.GetInt32()),
            Done = done.Select(value => value.GetBoolean()),
            Bits = bits.SelectMany(value => value.GetInt32()),
            Repeated = repeated.Select(value => value.GetInt32()),
            Unlocked = unlocked.Select(value => value.GetBoolean())
        };
    }
}
