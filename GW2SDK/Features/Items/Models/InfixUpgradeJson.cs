using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class InfixUpgradeJson
{
    public static InfixUpgrade GetInfixUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember attributes = new("attributes");
        OptionalMember buff = new("buff");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(attributes.Name))
            {
                attributes = member;
            }
            else if (member.NameEquals(buff.Name))
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
            ItemstatsId = id.Select(value => value.GetInt32()),
            Attributes =
                attributes.SelectMany(value => value.GetUpgradeAttribute(missingMemberBehavior)),
            Buff = buff.Select(value => value.GetBuff(missingMemberBehavior))
        };
    }
}
