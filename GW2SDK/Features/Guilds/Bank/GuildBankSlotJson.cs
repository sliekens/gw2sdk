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

        RequiredMember<int> itemId = new("id");
        RequiredMember<int> count = new("count");

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
            ItemId = itemId.GetValue(),
            Count = count.GetValue()
        };
    }
}
