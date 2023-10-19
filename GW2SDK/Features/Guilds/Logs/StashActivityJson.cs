using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

[PublicAPI]
public static class StashActivityJson
{
    public static StashActivity GetStashActivity(
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
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(time.Name))
            {
                time = member;
            }
            else if (member.NameEquals(user.Name))
            {
                user = member;
            }
            else if (member.NameEquals(operation.Name))
            {
                operation = member;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId = member;
            }
            else if (member.NameEquals(count.Name))
            {
                count = member;
            }
            else if (member.NameEquals(coins.Name))
            {
                coins = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new StashActivity
        {
            Id = id.Map(value => value.GetInt32()),
            Time = time.Map(value => value.GetDateTimeOffset()),
            User = user.Map(value => value.GetStringRequired()),
            Operation =
                operation.Map(value => value.GetEnum<StashOperation>(missingMemberBehavior)),
            ItemId = itemId.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32()),
            Coins = coins.Map(value => value.GetInt32())
        };
    }
}
