using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class BonusJson
{
    public static Bonus GetBonus(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember type = "type";
        RequiredMember owner = "owner";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(type.Name))
            {
                type = member;
            }
            else if (member.NameEquals(owner.Name))
            {
                owner = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Bonus
        {
            Kind = type.Map(value => value.GetEnum<BonusKind>(missingMemberBehavior)),
            Owner = owner.Map(value => value.GetEnum<TeamColor>(missingMemberBehavior))
        };
    }
}
