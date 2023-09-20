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
        RequiredMember<int> id = new("id");
        RequiredMember<UpgradeAttribute> attributes = new("attributes");
        OptionalMember<Buff> buff = new("buff");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(attributes.Name))
            {
                attributes.Value = member.Value;
            }
            else if (member.NameEquals(buff.Name))
            {
                buff.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InfixUpgrade
        {
            ItemstatsId = id.GetValue(),
            Attributes =
                attributes.SelectMany(value => value.GetUpgradeAttribute(missingMemberBehavior)),
            Buff = buff.Select(value => value.GetBuff(missingMemberBehavior))
        };
    }
}
