using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats;

internal static class AttributeJson
{
    public static Attribute GetAttribute(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Attribute
        {
            Name = attribute.Map(static value => value.GetAttributeName()),
            Multiplier = multiplier.Map(static value => value.GetDouble()),
            Value = value.Map(static value => value.GetInt32())
        };
    }
}
