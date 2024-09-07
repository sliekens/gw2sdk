using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class BonusJson
{
    public static Bonus GetBonus(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Bonus
        {
            Kind = type.Map(static value => value.GetEnum<BonusKind>()),
            Owner = owner.Map(static value => value.GetEnum<TeamColor>())
        };
    }
}
