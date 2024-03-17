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
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("stash"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (time.Match(member))
            {
                time = member;
            }
            else if (user.Match(member))
            {
                user = member;
            }
            else if (operation.Match(member))
            {
                operation = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (coins.Match(member))
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
