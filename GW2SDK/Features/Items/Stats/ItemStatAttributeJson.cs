using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats;

internal static class ItemStatAttributeJson
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
            if (member.Name == attribute.Name)
            {
                attribute = member;
            }
            else if (member.Name == multiplier.Name)
            {
                multiplier = member;
            }
            else if (member.Name == amount.Name)
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
            Attribute = attribute.Map(value => value.GetAttributeName(missingMemberBehavior)),
            Multiplier = multiplier.Map(value => value.GetDouble()),
            Value = amount.Map(value => value.GetInt32())
        };
    }
}
