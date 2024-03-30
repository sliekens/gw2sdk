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
            if (type.Match(member))
            {
                type = member;
            }
            else if (owner.Match(member))
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
            Kind = type.Map(value => value.GetEnum<BonusKind>()),
            Owner = owner.Map(value => value.GetEnum<TeamColor>())
        };
    }
}
