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
        RequiredMember attribute = new("attribute");
        RequiredMember multiplier = new("multiplier");
        RequiredMember amount = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(attribute.Name))
            {
                attribute.Value = member.Value;
            }
            else if (member.NameEquals(multiplier.Name))
            {
                multiplier.Value = member.Value;
            }
            else if (member.NameEquals(amount.Name))
            {
                amount.Value = member.Value;
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
