using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class UpgradeAttributeJson
{
    public static UpgradeAttribute GetUpgradeAttribute(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember attribute = "attribute";
        RequiredMember modifier = "modifier";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(attribute.Name))
            {
                attribute = member;
            }
            else if (member.NameEquals(modifier.Name))
            {
                modifier = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UpgradeAttribute
        {
            Attribute = attribute.Map(value => value.GetEnum<UpgradeAttributeName>(missingMemberBehavior)),
            Modifier = modifier.Map(value => value.GetInt32())
        };
    }
}
