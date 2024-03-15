using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class AccountAchievementJson
{
    public static AccountAchievement GetAccountAchievement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember current = "current";
        RequiredMember max = "max";
        RequiredMember done = "done";
        OptionalMember bits = "bits";
        NullableMember repeated = "repeated";
        NullableMember unlocked = "unlocked";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (current.Match(member))
            {
                current = member;
            }
            else if (max.Match(member))
            {
                max = member;
            }
            else if (done.Match(member))
            {
                done = member;
            }
            else if (bits.Match(member))
            {
                bits = member;
            }
            else if (repeated.Match(member))
            {
                repeated = member;
            }
            else if (unlocked.Match(member))
            {
                unlocked = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AccountAchievement
        {
            Id = id.Map(value => value.GetInt32()),
            Current = current.Map(value => value.GetInt32()),
            Max = max.Map(value => value.GetInt32()),
            Done = done.Map(value => value.GetBoolean()),
            Bits = bits.Map(values => values.GetList(value => value.GetInt32())),
            Repeated = repeated.Map(value => value.GetInt32()).GetValueOrDefault(),
            Unlocked = unlocked.Map(value => value.GetBoolean()).GetValueOrDefault(true)
        };
    }
}
