using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class TreasuryDepositJson
{
    public static TreasuryDeposit GetTreasuryDeposit(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember user = "user";
        RequiredMember itemId = "item_id";
        RequiredMember count = "count";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("treasury"))
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
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TreasuryDeposit
        {
            Id = id.Map(static value => value.GetInt32()),
            Time = time.Map(static value => value.GetDateTimeOffset()),
            User = user.Map(static value => value.GetStringRequired()),
            ItemId = itemId.Map(static value => value.GetInt32()),
            Count = count.Map(static value => value.GetInt32())
        };
    }
}
