using System.Text.Json;

using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats;

internal static class AttributeJson
{
    public static AttributeValue GetAttribute(this in JsonElement json)
    {
        RequiredMember attribute = "attribute";
        RequiredMember multiplier = "multiplier";
        RequiredMember value = "value";

        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AttributeValue
        {
            Name = attribute.Map(static (in value) => value.GetAttributeName()),
            Multiplier = multiplier.Map(static (in value) => value.GetDouble()),
            Value = value.Map(static (in value) => value.GetInt32())
        };
    }
}
