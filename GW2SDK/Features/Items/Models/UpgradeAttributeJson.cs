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
        RequiredMember<UpgradeAttributeName> attribute = new("attribute");
        RequiredMember<int> modifier = new("modifier");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(attribute.Name))
            {
                attribute.Value = member.Value;
            }
            else if (member.NameEquals(modifier.Name))
            {
                modifier.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UpgradeAttribute
        {
            Attribute = attribute.GetValue(missingMemberBehavior),
            Modifier = modifier.GetValue()
        };
    }
}
