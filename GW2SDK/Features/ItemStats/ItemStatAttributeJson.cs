using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.ItemStats;

[PublicAPI]
public static class ItemStatAttributeJson
{
    public static ItemStatAttribute GetItemStatAttribute(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember attribute = "attribute";
        RequiredMember multiplier = "multiplier";
        RequiredMember amount = "value";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(attribute.Name))
            {
                attribute = member;
            }
            else if (member.NameEquals(multiplier.Name))
            {
                multiplier = member;
            }
            else if (member.NameEquals(amount.Name))
            {
                amount = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemStatAttribute
        {
            Attribute = attribute.Select(value => value.GetEnum<UpgradeAttributeName>(missingMemberBehavior)),
            Multiplier = multiplier.Select(value => value.GetDouble()),
            Value = amount.Select(value => value.GetInt32())
        };
    }
}
