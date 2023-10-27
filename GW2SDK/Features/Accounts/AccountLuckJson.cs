using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Accounts;

internal static class AccountLuckJson
{
    public static AccountLuck GetAccountLuck(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        JsonElement luckObj = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (luckObj.ValueKind == JsonValueKind.Undefined)
            {
                luckObj = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        // If an account has no luck an empty array ([]) will be returned.
        if (luckObj.ValueKind == JsonValueKind.Undefined)
        {
            return new AccountLuck { Luck = 0 };
        }

        // The endpoint returns an array with a singular object containing the following:
        // id (string) – The string "luck".
        // value (number) – The amount of luck consumed
        RequiredMember value = "value";
        foreach (var member in luckObj.EnumerateObject())
        {
            if (member.NameEquals("id"))
            {
                if (!member.Value.ValueEquals("luck"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(value.Name))
            {
                value = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AccountLuck { Luck = value.Map(luck => luck.GetInt32()) };
    }
}
