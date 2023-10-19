using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Accounts;

[PublicAPI]
public static class AccountLuckJson
{
    public static AccountLuck GetAccountLuck(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // The response is an array due to the way luck is stored internally.
        // If an account has no luck an empty array ([]) will be returned.
        if (json.GetArrayLength() == 0)
        {
            return new AccountLuck { Luck = 0 };
        }

        // The endpoint returns an array with a singular object containing the following:
        // id (string) – The string "luck".
        // value (number) – The amount of luck consumed
        RequiredMember value = "value";

        foreach (var member in json[0].EnumerateObject())
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
