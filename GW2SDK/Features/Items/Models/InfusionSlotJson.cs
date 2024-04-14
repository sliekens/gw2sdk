using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfusionSlotJson
{
    public static InfusionSlot GetInfusionSlot(
        this JsonElement json
    )
    {
        RequiredMember flags = "flags";
        NullableMember itemId = "item_id";
        foreach (var member in json.EnumerateObject())
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InfusionSlot
        {
            Flags = flags.Map(static values => values.GetInfusionSlotFlags()),
            ItemId = itemId.Map(static value => value.GetInt32())
        };
    }
}
