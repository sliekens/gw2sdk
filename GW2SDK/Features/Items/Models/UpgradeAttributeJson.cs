using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class UpgradeAttributeJson
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

        return new UpgradeAttribute
        {
            Attribute =
                attribute.Map(
                    value => value.GetEnum<AttributeName>(missingMemberBehavior)
                ),
            Modifier = modifier.Map(value => value.GetInt32())
        };
    }
}
