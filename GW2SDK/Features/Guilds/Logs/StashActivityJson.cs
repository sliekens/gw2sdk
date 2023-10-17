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
        RequiredMember<int> id = new("id");
        RequiredMember<DateTimeOffset> time = new("time");
        RequiredMember<string> user = new("user");
        RequiredMember<StashOperation> operation = new("operation");
        RequiredMember<int> itemId = new("item_id");
        RequiredMember<int> count = new("count");
        RequiredMember<Coin> coins = new("coins");

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
            else if (member.NameEquals(operation.Name))
            {
                operation.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (member.NameEquals(coins.Name))
            {
                coins.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new StashActivity
        {
            Id = id.GetValue(),
            Time = time.GetValue(),
            User = user.GetValue(),
            Operation = operation.GetValue(missingMemberBehavior),
            ItemId = itemId.GetValue(),
            Count = count.GetValue(),
            Coins = coins.GetValue()
        };
    }
}
