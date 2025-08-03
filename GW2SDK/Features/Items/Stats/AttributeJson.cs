using System.Text.Json;

using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats;

internal static class AttributeJson
{
    public static Attribute GetAttribute(this in JsonElement json)
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

        return new Attribute
        {
            Name = attribute.Map(static (in JsonElement value) => value.GetAttributeName()),
            Multiplier = multiplier.Map(static (in JsonElement value) => value.GetDouble()),
            Value = value.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
