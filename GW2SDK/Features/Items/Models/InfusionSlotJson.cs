using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfusionSlotJson
{
    public static InfusionSlot GetInfusionSlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember flags = "flags";
        NullableMember itemId = "item_id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == itemId.Name)
            {
                itemId = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InfusionSlot
        {
            Flags = flags.Map(values => values.GetInfusionSlotFlags()),
            ItemId = itemId.Map(value => value.GetInt32())
        };
    }
}
