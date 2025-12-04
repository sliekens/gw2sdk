using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfusionSlotJson
{
    public static InfusionSlot GetInfusionSlot(this in JsonElement json)
    {
        RequiredMember flags = "flags";
        NullableMember itemId = "item_id";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (flags.Match(member))
            {
                flags = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new InfusionSlot
        {
            Flags = flags.Map(static (in values) => values.GetInfusionSlotFlags()),
            ItemId = itemId.Map(static (in value) => value.GetInt32())
        };
    }
}
