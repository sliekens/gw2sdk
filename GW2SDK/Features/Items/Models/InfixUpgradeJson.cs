using System.Text.Json;
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
            Attributes =
                attributes.Map(
                    values => values.GetList(
                        value => value.GetUpgradeAttribute(missingMemberBehavior)
                    )
                ),
            Buff = buff.Map(value => value.GetBuff(missingMemberBehavior))
        };
    }
}
