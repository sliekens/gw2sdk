using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfixUpgradeJson
{
    public static IDictionary<AttributeName, int> GetAttributes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var attributes = new Dictionary<AttributeName, int>(json.GetArrayLength());
        foreach (var entry in json.EnumerateArray())
        {
            RequiredMember attribute = "attribute";
            RequiredMember modifier = "modifier";
            foreach (var member in entry.EnumerateObject())
            {
                if (member.Name == attribute.Name)
                {
                    attribute = member;
                }
                else if (member.Name == modifier.Name)
                {
                    modifier = member;
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            var key = attribute.Map(value => value.GetAttributeName(missingMemberBehavior));
            var value = modifier.Map(value => value.GetInt32());
            attributes.Add(key, value);
        }

        return attributes;
    }
}
