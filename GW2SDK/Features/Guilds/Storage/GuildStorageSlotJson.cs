using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Storage;

internal static class GuildStorageSlotJson
{
    public static GuildStorageSlot GetGuildStorageSlot(this in JsonElement json)
    {
        RequiredMember itemId = "id";
        RequiredMember count = "count";

        foreach (var member in json.EnumerateObject())
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

        return new GuildStorageSlot
        {
            ItemId = itemId.Map(static (in JsonElement value) => value.GetInt32()),
            Count = count.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
