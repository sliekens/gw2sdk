using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class InfusionSlotJson
{
    public static InfusionSlot GetInfusionSlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<InfusionSlotFlag> flags = new("flags");
        NullableMember<int> itemId = new("item_id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InfusionSlot
        {
            Flags = flags.GetValues(missingMemberBehavior),
            ItemId = itemId.GetValue()
        };
    }
}
