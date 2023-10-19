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
        RequiredMember flags = "flags";
        NullableMember itemId = "item_id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(itemId.Name))
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
            Flags = flags.SelectMany(value => value.GetEnum<InfusionSlotFlag>(missingMemberBehavior)),
            ItemId = itemId.Select(value => value.GetInt32())
        };
    }
}
