using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class InfixUpgradeJson
{
    public static InfixUpgrade GetInfixUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember attributes = "attributes";
        OptionalMember buff = "buff";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == attributes.Name)
            {
                attributes = member;
            }
            else if (member.Name == buff.Name)
            {
                buff = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InfixUpgrade
        {
            AttributeCombinationId = id.Map(value => value.GetInt32()),
            Attributes = attributes.Map(values => values.GetAttributes(missingMemberBehavior)),
            Buff = buff.Map(value => value.GetBuff(missingMemberBehavior))
        };
    }

    private static IDictionary<AttributeName, int> GetAttributes(
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
