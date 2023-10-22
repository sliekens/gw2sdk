using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Abilities;

internal static class AbilityRankJson
{
    public static AbilityRank GetAbilityRank(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember cost = "cost";
        RequiredMember effect = "effect";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(cost.Name))
            {
                cost = member;
            }
            else if (member.NameEquals(effect.Name))
            {
                effect = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AbilityRank
        {
            Cost = cost.Map(value => value.GetInt32()),
            Effect = effect.Map(value => value.GetStringRequired())
        };
    }
}
