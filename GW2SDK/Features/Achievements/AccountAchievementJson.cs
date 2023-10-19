﻿using System.Text.Json;
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
        RequiredMember id = "id";
        RequiredMember current = "current";
        RequiredMember max = "max";
        RequiredMember done = "done";
        OptionalMember bits = "bits";
        NullableMember repeated = "repeated";
        NullableMember unlocked = "unlocked";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(current.Name))
            {
                current = member;
            }
            else if (member.NameEquals(max.Name))
            {
                max = member;
            }
            else if (member.NameEquals(done.Name))
            {
                done = member;
            }
            else if (member.NameEquals(bits.Name))
            {
                bits = member;
            }
            else if (member.NameEquals(repeated.Name))
            {
                repeated = member;
            }
            else if (member.NameEquals(unlocked.Name))
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
