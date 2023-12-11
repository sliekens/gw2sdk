using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class GuildBankActivityJson
{
    public static GuildBankActivity GetGuildBankActivity(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember user = "user";
        RequiredMember operation = "operation";
        RequiredMember itemId = "item_id";
        RequiredMember count = "count";
        RequiredMember coins = "coins";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("stash"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == time.Name)
            {
                time = member;
            }
            else if (member.Name == user.Name)
            {
                user = member;
            }
            else if (member.Name == operation.Name)
            {
                operation = member;
            }
            else if (member.Name == itemId.Name)
            {
                itemId = member;
            }
            else if (member.Name == count.Name)
            {
                count = member;
            }
            else if (member.Name == coins.Name)
            {
                coins = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildBankActivity
        {
            Id = id.Map(value => value.GetInt32()),
            Time = time.Map(value => value.GetDateTimeOffset()),
            User = user.Map(value => value.GetStringRequired()),
            Operation =
                operation.Map(
                    value => value.GetEnum<GuildBankOperationKind>(missingMemberBehavior)
                ),
            ItemId = itemId.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32()),
            Coins = coins.Map(value => value.GetInt32())
        };
    }
}
