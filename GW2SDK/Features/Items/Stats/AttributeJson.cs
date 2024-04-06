using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats;

internal static class AttributeJson
{
    public static Attribute GetAttribute(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember attribute = "attribute";
        RequiredMember multiplier = "multiplier";
        RequiredMember value = "value";

        foreach (var member in json.EnumerateObject())
        {
            if (attribute.Match(member))
            {
                attribute = member;
            }
            else if (multiplier.Match(member))
            {
                multiplier = member;
            }
            else if (value.Match(member))
            {
                value = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Attribute
        {
            Name = attribute.Map(value => value.GetAttributeName()),
            Multiplier = multiplier.Map(value => value.GetDouble()),
            Value = value.Map(value => value.GetInt32())
        };
    }
}
