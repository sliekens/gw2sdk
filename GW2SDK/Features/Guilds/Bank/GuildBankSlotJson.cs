using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Bank;

[PublicAPI]
public static class GuildBankSlotJson
{
    public static GuildBankSlot? GetGuildBankSlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        RequiredMember itemId = new("id");
        RequiredMember count = new("count");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(itemId.Name))
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

        return new GuildBankSlot
        {
            ItemId = itemId.Select(value => value.GetInt32()),
            Count = count.Select(value => value.GetInt32())
        };
    }
}
