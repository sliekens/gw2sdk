using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Bank;

internal static class GuildBankSlotJson
{
    public static GuildBankSlot? GetGuildBankSlot(this in JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        RequiredMember itemId = "id";
        RequiredMember count = "count";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildBankSlot
        {
            ItemId = itemId.Map(static (in value) => value.GetInt32()),
            Count = count.Map(static (in value) => value.GetInt32())
        };
    }
}
