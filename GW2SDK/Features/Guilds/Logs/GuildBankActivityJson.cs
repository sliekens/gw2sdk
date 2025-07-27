using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class GuildBankActivityJson
{
    public static GuildBankActivity GetGuildBankActivity(this in JsonElement json)
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
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildBankActivity
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Time = time.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            User = user.Map(static (in JsonElement value) => value.GetStringRequired()),
            Operation = operation.Map(static (in JsonElement value) => value.GetEnum<GuildBankOperationKind>()),
            ItemId = itemId.Map(static (in JsonElement value) => value.GetInt32()),
            Count = count.Map(static (in JsonElement value) => value.GetInt32()),
            Coins = coins.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
