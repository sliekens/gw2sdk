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
        RequiredMember<UpgradeAttributeName> attribute = new("attribute");
        RequiredMember<double> multiplier = new("multiplier");
        RequiredMember<int> value = new("value");

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
            else if (member.NameEquals(value.Name))
            {
                value.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemStatAttribute
        {
            Attribute = attribute.GetValue(missingMemberBehavior),
            Multiplier = multiplier.GetValue(),
            Value = value.GetValue()
        };
    }
}
