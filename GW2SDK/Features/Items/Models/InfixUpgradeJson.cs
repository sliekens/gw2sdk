using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfixUpgradeJson
{
    public static IDictionary<Extensible<AttributeName>, int> GetAttributes(this JsonElement json)
    {
        var attributes = new Dictionary<Extensible<AttributeName>, int>(json.GetArrayLength());
        foreach (var entry in json.EnumerateArray())
        {
            RequiredMember attribute = "attribute";
            RequiredMember modifier = "modifier";
            foreach (var member in entry.EnumerateObject())
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

            var key = attribute.Map(static value => value.GetAttributeName());
            var value = modifier.Map(static value => value.GetInt32());
            attributes.Add(key, value);
        }

        return attributes;
    }
}
