using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfixUpgradeJson
{
    public static ValueDictionary<Extensible<AttributeName>, int> GetAttributes(
        this in JsonElement json
    )
    {
        ValueDictionary<Extensible<AttributeName>, int> attributes = new(json.GetArrayLength());
        foreach (JsonElement entry in json.EnumerateArray())
        {
            RequiredMember attribute = "attribute";
            RequiredMember modifier = "modifier";
            foreach (JsonProperty member in entry.EnumerateObject())
            {
                if (attribute.Match(member))
                {
                    attribute = member;
                }
                else if (modifier.Match(member))
                {
                    modifier = member;
                }
                else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedMember(member.Name);
                }
            }

            Extensible<AttributeName> key = attribute.Map(static (in JsonElement value) => value.GetAttributeName());
            var value = modifier.Map(static (in JsonElement value) => value.GetInt32());
            attributes.Add(key, value);
        }

        return attributes;
    }
}
