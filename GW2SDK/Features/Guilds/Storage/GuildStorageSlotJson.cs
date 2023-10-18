using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Storage;

[PublicAPI]
public static class GuildStorageSlotJson
{
    public static GuildStorageSlot GetGuildStorageSlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
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

        return new GuildStorageSlot
        {
            ItemId = itemId.GetValue(),
            Count = count.GetValue(),
        };
    }
}
