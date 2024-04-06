using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfixUpgradeJson
{
    public static IDictionary<Extensible<AttributeName>, int> GetAttributes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            var key = attribute.Map(value => value.GetAttributeName());
            var value = modifier.Map(value => value.GetInt32());
            attributes.Add(key, value);
        }

        return attributes;
    }
}
