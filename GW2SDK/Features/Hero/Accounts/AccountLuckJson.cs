using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class AccountLuckJson
{
    public static AccountLuck GetAccountLuck(this in JsonElement json)
    {
        JsonElement luckObj = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (luckObj.ValueKind == JsonValueKind.Undefined)
            {
                luckObj = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
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
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (value.Match(member))
            {
                value = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AccountLuck { Luck = value.Map(static (in JsonElement luck) => luck.GetInt32()) };
    }
}
