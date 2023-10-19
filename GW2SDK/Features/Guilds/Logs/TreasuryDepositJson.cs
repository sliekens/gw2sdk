using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

[PublicAPI]
public static class TreasuryDepositJson
{
    public static TreasuryDeposit GetTreasuryDeposit(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember time = new("time");
        RequiredMember user = new("user");
        RequiredMember itemId = new("item_id");
        RequiredMember count = new("count");

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
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(time.Name))
            {
                time.Value = member.Value;
            }
            else if (member.NameEquals(user.Name))
            {
                user.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TreasuryDeposit
        {
            Id = id.Select(value => value.GetInt32()),
            Time = time.Select(value => value.GetDateTimeOffset()),
            User = user.Select(value => value.GetStringRequired()),
            ItemId = itemId.Select(value => value.GetInt32()),
            Count = count.Select(value => value.GetInt32())
        };
    }
}
